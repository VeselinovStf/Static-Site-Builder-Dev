using ApplicationCore.Entities.WidjetsEntityAggregate;
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
            ILoggerFactory loggerFactory,
            int? retry = 0)
        {
            int retryAvailibility = retry.Value;

            try
            {
                ssbDbContext.Database.Migrate();

                if (!ssbDbContext.Roles.Any())
                {
                    await ssbDbContext.Roles.AddRangeAsync(
                         GetPreconfiguredRoles());

                    await ssbDbContext.SaveChangesAsync();
                }

                if (!ssbDbContext.Users.Any())
                {
                    await ssbDbContext.Users.AddAsync(
                        GetPreconfiguredAdmin(ssbDbContext));

                    await ssbDbContext.Users.AddAsync(
                        GetPreconfiguredClient(ssbDbContext));

                    await ssbDbContext.SaveChangesAsync();
                }

                if (!ssbDbContext.Widjets.Any())
                {
                    await ssbDbContext.Widjets.AddRangeAsync(
                        GetPreconfiguredWidjets());

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
                    await SeedAsync(ssbDbContext, loggerFactory, retryAvailibility);
                }
            }
        }

        private static Widjet[] GetPreconfiguredWidjets()
        {
            var menuConfigWidjet = new Widjet()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "This widjet gives you the ability to change the display of meny",
                Functionality = "menu-config",
                IsFree = true,
                IsOn = true,
                Key = "",
                Name = "MenuConfig",
                Price = 0m,
                Version = 1,
                Votes = 0,
            };

            return new Widjet[] { menuConfigWidjet };
        }

        private static IdentityRole[] GetPreconfiguredRoles()
        {
            var administrator = new IdentityRole() { Id = "1", Name = "Administrator", NormalizedName = "ADMINISTRATOR" };
            var client = new IdentityRole() { Id = "2", Name = "Client", NormalizedName = "CLIENT" };

            return new IdentityRole[] { administrator, client };
        }

        private static ApplicationUser GetPreconfiguredClient(SSBDbContext dbContext)
        {
            var clientId = Guid.NewGuid().ToString();

            var mailBox = new ApplicationCore.Entities.MessageAggregate.MailBox()
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = clientId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsDeleted = false
            };

            var clientUser = new ApplicationUser()
            {
                Id = clientId,
                UserName = "Client",
                NormalizedUserName = "CLIENT".ToUpper(),
                Email = "client@mail.com",
                TwoFactorEnabled = false,
                NormalizedEmail = "client@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+359359",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                MailBox = mailBox,
                Project = new ApplicationCore.Entities.SiteProjectAggregate.Project()
                {
                    ClientId = clientId,
                }
            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(clientUser, "!Aa12345678");
            clientUser.PasswordHash = hashePass;

            dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = 2.ToString(),
                UserId = clientUser.Id
            }).Wait();

            dbContext.MailBoxes.AddAsync(mailBox);

            return clientUser;
        }

        private static ApplicationUser GetPreconfiguredAdmin(SSBDbContext dbContext)
        {
            var adminId = Guid.NewGuid().ToString();

            var mailBox = new ApplicationCore.Entities.MessageAggregate.MailBox()
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = adminId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsDeleted = false
            };
            var adminUser = new ApplicationUser()
            {
                Id = adminId,
                UserName = "Admin",
                NormalizedUserName = "ADMIN".ToUpper(),
                Email = "admin@mail.com",
                TwoFactorEnabled = false,
                NormalizedEmail = "admin@mail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+359359",
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                AccessFailedCount = 0,
                LockoutEnabled = false,
                MailBox = mailBox,
                Project = new ApplicationCore.Entities.SiteProjectAggregate.Project()
                {
                    ClientId = adminId
                }
            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "!Aa12345678");
            adminUser.PasswordHash = hashePass;

            dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = 1.ToString(),
                UserId = adminUser.Id
            }).Wait();

            dbContext.MailBoxes.AddAsync(mailBox);

            return adminUser;
        }
    }
}