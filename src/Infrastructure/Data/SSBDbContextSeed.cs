using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SiteType;
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
                       await GetPreconfiguredAdmin(ssbDbContext));

                    await ssbDbContext.Users.AddAsync(
                        await GetPreconfiguredClient(ssbDbContext));

                    await ssbDbContext.SaveChangesAsync();
                }

                if (!ssbDbContext.Widjets.Any())
                {
                    await ssbDbContext.Widjets.AddRangeAsync(
                        GetPreconfiguredWidjets());

                    await ssbDbContext.SaveChangesAsync();
                }

                if (!ssbDbContext.SiteTypes.Any())
                {
                    await ssbDbContext.SiteTypes.AddRangeAsync(
                        GetPreconfiguredSiteTypes());

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

        private static SiteType[] GetPreconfiguredSiteTypes()
        {
            var storeType = new SiteType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Multipurpose eCommerce Site",
                Description = "Build you owne eCommersce site, sell one nich or many all depends on you. Use build in Widjets to customize and optimize your new application. Start earning in few hours.",
                Type = SiteTypesEnum.StoreType
            };

            var blogType = new SiteType()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Blog Site",
                Description = "Build you owne blog site. Use build in Widjets to customize and optimize your new application. Create your first posts in minutes. Start posting now.",
                Type = SiteTypesEnum.BlogType
            };

            return new SiteType[] { storeType, blogType };
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

        private static async Task<ApplicationUser> GetPreconfiguredClient(SSBDbContext dbContext)
        {
            var clientId = Guid.NewGuid().ToString();
            var projectId = Guid.NewGuid().ToString();

            var mailBox = new ApplicationCore.Entities.MessageAggregate.MailBox()
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = clientId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsDeleted = false
            };

            var clientProject = new Project()
            {
                Id = projectId,
                ClientId = clientId,
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
                Project = clientProject
            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(clientUser, "!Aa12345678");
            clientUser.PasswordHash = hashePass;

            dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = 2.ToString(),
                UserId = clientUser.Id
            }).Wait();

            await dbContext.MailBoxes.AddAsync(mailBox);
            await dbContext.Projects.AddAsync(clientProject);

            return clientUser;
        }

        private static async Task<ApplicationUser> GetPreconfiguredAdmin(SSBDbContext dbContext)
        {
            var adminId = Guid.NewGuid().ToString();
            var projectId = Guid.NewGuid().ToString();

            var mailBox = new ApplicationCore.Entities.MessageAggregate.MailBox()
            {
                Id = Guid.NewGuid().ToString(),
                ClientId = adminId,
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now,
                IsDeleted = false
            };

            var adminProject = new Project()
            {
                Id = projectId,
                ClientId = adminId,
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
                Project = adminProject
            };

            var hashePass = new PasswordHasher<ApplicationUser>().HashPassword(adminUser, "!Aa12345678");
            adminUser.PasswordHash = hashePass;

            dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = 1.ToString(),
                UserId = adminUser.Id
            }).Wait();

            await dbContext.MailBoxes.AddAsync(mailBox);
            await dbContext.Projects.AddAsync(adminProject);

            return adminUser;
        }
    }
}