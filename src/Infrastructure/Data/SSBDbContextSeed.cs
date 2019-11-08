using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SSBDbContextSeed
    {
        public static async Task SeedAsync(
            SSBDbContext ssbDbContext,
            ModelBuilder builder,
            ILoggerFactory loggerFactory,
            int? retry = 0)
        {
            int retryAvailibility = retry.Value;

            try
            {
                ssbDbContext.Database.Migrate();

                if (!ssbDbContext.Roles.Any())
                {
                    ssbDbContext.Roles.AddRange(
                        GetPreconfiguredRoles());

                    await ssbDbContext.SaveChangesAsync();
                }

                if (!ssbDbContext.Users.Any())
                {
                    ssbDbContext.Users.Add(
                        GetPreconfiguredAdmin(builder));

                    ssbDbContext.Users.Add(
                        GetPreconfiguredClient(builder));

                    await ssbDbContext.SaveChangesAsync();
                }
               
            }
            catch (Exception ex)
            {
                if (retryAvailibility < 10)
                {
                    retryAvailibility++;
                    var log = loggerFactory.CreateLogger<SSBDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(ssbDbContext,builder, loggerFactory, retryAvailibility);
                }
            }
        }

        private static IdentityRole[] GetPreconfiguredRoles()
        {
            var administrator = new IdentityRole() { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
            var client = new IdentityRole() { Id = "2", Name = "Client", NormalizedName = "CLIENT" };

            return new IdentityRole[] { administrator, client };
        }

        private static ApplicationUser GetPreconfiguredClient(ModelBuilder builder)
        {

            var clientUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Client",
                NormalizedUserName = "client@mail.com".ToUpper(),
                Email = "client@mail.com",
                TwoFactorEnabled = false,
                NormalizedEmail = "client@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+359359",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                AccessFailedCount = 0,
                LockoutEnabled = false,

            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(clientUser, "!Aa12345678");
            clientUser.PasswordHash = hashePass;

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = 2.ToString(),
                UserId = clientUser.Id
            });

            return clientUser;

        }

        private static ApplicationUser GetPreconfiguredAdmin(ModelBuilder builder)
        {

            var adminUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                NormalizedUserName = "admin@mail.com".ToUpper(),
                Email = "admin@mail.com",
                TwoFactorEnabled = false,
                NormalizedEmail = "admin@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+359359",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                AccessFailedCount = 0,
                LockoutEnabled = false
            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "!Aa12345678");
            adminUser.PasswordHash = hashePass;

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = 1.ToString(),
                UserId = adminUser.Id
            });

            return adminUser;
        }
    }
}
