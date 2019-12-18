using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.Wallet;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Services.FileTransferrer.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class SSBDbContextSeed
    {
        public static async Task SeedAsync(
            SSBDbContext ssbDbContext,
            ILoggerFactory loggerFactory,
            IFileTransferrer<ConvertedFileElement> fileTransporter,
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

                ////if (!ssbDbContext.Widjets.Any())
                ////{
                ////    await ssbDbContext.Widjets.AddRangeAsync(
                ////        GetPreconfiguredWidjets());

                ////    await ssbDbContext.SaveChangesAsync();
                ////}

                if (!ssbDbContext.Users.Any())
                {
                    await ssbDbContext.Users.AddAsync(
                       await GetPreconfiguredAdmin(ssbDbContext));

                    await ssbDbContext.Users.AddAsync(
                        await GetPreconfiguredClient(ssbDbContext));

                    await ssbDbContext.SaveChangesAsync();
                }
               

                //if (!ssbDbContext.SiteTypes.Any())
                //{
                //    await ssbDbContext.SiteTypes.AddRangeAsync(
                //        GetPreconfiguredSiteTypes());

                //    await ssbDbContext.SaveChangesAsync();
                //}

                ////if (!ssbDbContext.SiteTemplates.Any())
                ////{
                ////    await ssbDbContext.SiteTemplates.AddRangeAsync(
                ////        await DevelopmentAddPreBuildSiteTemplatesFromDirectory(fileTransporter, ssbDbContext));

                ////    await ssbDbContext.SaveChangesAsync();
                ////}

              
                //await CustomWidgetUpdater(ssbDbContext);
            }
            catch (Exception ex)
            {
                if (retryAvailibility < 10)
                {
                    retryAvailibility++;
                    var log = loggerFactory.CreateLogger<SSBDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(ssbDbContext, loggerFactory, fileTransporter, retryAvailibility);
                }
            }
        }

       
        private static async Task CustomWidgetUpdater(SSBDbContext ssbDbContext)
        {
            //Add new wid
            var widjet = new Widget()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "Test Widjet01",
                Functionality = "test-widget",
                IsFree = false,
                IsOn = false,
                Key = "",
                Name = "TestWidget",
                Price = 2.0m,
                Version = 1,
                Votes = 0,
                SiteTypeSpecification = SiteTypesEnum.StoreType,
                SystemName = SiteWidgetEnum.None,
                Dependency = SiteWidgetEnum.None
            };

            

            if (ssbDbContext.Widjets.FirstOrDefault(w => w.Name == widjet.Name) == null)
            {
                await ssbDbContext.Widjets.AddAsync(widjet);

                await ssbDbContext.SaveChangesAsync();
            }
        }

        private static async Task<SiteTemplate[]> DevelopmentAddPreBuildSiteTemplatesFromDirectory(
            IFileTransferrer<ConvertedFileElement> fileTransporter, SSBDbContext dbContext)
        {
            var defaultStoreTypeSiteFileRead = await fileTransporter.FilesToList(@"H:\HUB\Static_Store_Builder-SSB-\Dev_V03\src\Web\BuildInTemplates\StoreTemplates\Default");

            var storePreconfirmedWidjets =  dbContext.Widjets;

            var storeTypeId = Guid.NewGuid().ToString();
            var storeWidgets = new List<Widget>(storePreconfirmedWidjets.Where(w => w.SiteTypeSpecification == SiteTypesEnum.StoreType));
            var storeType = new SiteType()
            {
                Id = storeTypeId,
                Name = "Multipurpose eCommerce Site",
                Description = "Build you owne eCommersce site, sell one nich or many all depends on you. Use build in Widjets to customize and optimize your new application. Start earning in few hours.",
                Type = SiteTypesEnum.StoreType,
                UsebleWidjets = new List<SiteTypeWidget>(storeWidgets.Select(w => new SiteTypeWidget()
                {
                    SiteTypeId = storeTypeId,
                    WidgetId = w.Id,
                    Widget = w
                }))
            };

            var blogTypeId = Guid.NewGuid().ToString();
            var blogWidgets = new List<Widget>(storePreconfirmedWidjets.Where(w => w.SiteTypeSpecification == SiteTypesEnum.BlogType));
            var blogType = new SiteType()
            {
                Id = blogTypeId,
                Name = "Blog Site",
                Description = "Build you owne blog site. Use build in Widjets to customize and optimize your new application. Create your first posts in minutes. Start posting now.",
                Type = SiteTypesEnum.BlogType,
                UsebleWidjets = new List<SiteTypeWidget>(blogWidgets.Select(w => new SiteTypeWidget()
                {
                    SiteTypeId = blogTypeId,
                    WidgetId = w.Id,
                    Widget = w
                }))
            };

            await dbContext.SiteTypes.AddAsync(storeType);
            await dbContext.SiteTypes.AddAsync(blogType);

            var defaultStoreSiteTemplate = new SiteTemplate()
            {
                Name = "DefaultStoreTemplate",
                Description = "Build in Default Multy Type Store Template",
                SiteTypeId = storeTypeId,
                SiteType = storeType
            };

            defaultStoreTypeSiteFileRead
                .ToList()
                .ForEach(
                    d => defaultStoreSiteTemplate
                    .AddElement(
                        d.FilePath, d.FileContent
                        )
                 );

            return new SiteTemplate[] { defaultStoreSiteTemplate };
        }

        private static Widget[] GetPreconfiguredWidjets()
        {
            var menuConfigWidjet = new Widget()
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
                SiteTypeSpecification = SiteTypesEnum.StoreType,
                SystemName = SiteWidgetEnum.MenuDisplay,
                Dependency = SiteWidgetEnum.None
            };

            var siteStructureWidjet = new Widget()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "This widjet gives you the ability to change the structure of site",
                Functionality = "site-structure",
                IsFree = true,
                IsOn = true,
                Key = "",
                Name = "SiteStructure",
                Price = 0m,
                Version = 1,
                Votes = 0,
                SiteTypeSpecification = SiteTypesEnum.StoreType,
                SystemName = SiteWidgetEnum.SiteStructure,
                Dependency = SiteWidgetEnum.None
            };

            var productsWidjet = new Widget()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "This widjet gives you the ability add sellible products to your site",
                Functionality = "products",
                IsFree = true,
                IsOn = true,
                Key = "",
                Name = "Products",
                Price = 0m,
                Version = 1,
                Votes = 0,
                SiteTypeSpecification = SiteTypesEnum.StoreType,
                SystemName = SiteWidgetEnum.Products,
                Dependency = SiteWidgetEnum.None
            };

            var topProductsWidjet = new Widget()
            {
                Id = Guid.NewGuid().ToString(),
                Description = "This widjet gives you the ability to display sellible products in different ways",
                Functionality = "top-products",
                IsFree = true,
                IsOn = true,
                Key = "",
                Name = "TopProducts",
                Price = 0m,
                Version = 1,
                Votes = 0,
                SiteTypeSpecification = SiteTypesEnum.StoreType,
                SystemName = SiteWidgetEnum.TopProducts,
                Dependency = SiteWidgetEnum.Products
            };

            return new Widget[] { menuConfigWidjet, siteStructureWidjet, productsWidjet, topProductsWidjet };
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
            var clientClientWidgetId = Guid.NewGuid().ToString();

            var clientUserBuildInWidjets = dbContext.Widjets.Where(w => w.IsFree == true).ToList();           

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
                Project = clientProject,
                ClientWidjets = new ApplicationUserWidgets()
                {
                   
                    ClientId = clientId,
                    ClientWidgets = new List<ClientWidgets>(clientUserBuildInWidjets.Select(a => new ClientWidgets()
                    {
                        WidgetId = a.Id,
                        
                    }))

                },
                Wallet = new ApplicationCore.Entities.Wallet.Wallet()
                {
                     ClientId = clientId,
                     AvailibleCredit = 5.0m,                     
                }
            };

            //foreach (var widget in clientUserBuildInWidjets)
            //{
            //    clientUser.ClientWidjets.AddWidjet(widget.Name, widget.Description, widget.Functionality,
            //        widget.Price, widget.Version, widget.Votes, widget.IsOn, widget.IsFree, widget.SystemName, widget.Dependency,
            //        widget.SiteTypeSpecification, widget.Key, widget.UsebleSiteTypeId);
            //}

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
            var adminClientWidgetId = Guid.NewGuid().ToString();

            var adminBuildInWidjets = dbContext.Widjets.ToList();

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
                Project = adminProject,
                ClientWidjets = new ApplicationUserWidgets()
                {
                    
                    ClientId = adminId,
                    ClientWidgets = new List<ClientWidgets>(adminBuildInWidjets.Select(a => new ClientWidgets()
                    {
                         WidgetId = a.Id,
                        
                    }))
                   
                }

            };

            //foreach (var widget in adminBuildInWidjets)
            //{
            //    adminUser.ClientWidjets.AddWidjet(widget.Name, widget.Description, widget.Functionality,
            //        widget.Price, widget.Version, widget.Votes, widget.IsOn, widget.IsFree, widget.SystemName, widget.Dependency,
            //        widget.SiteTypeSpecification, widget.Key, widget.UsebleSiteTypeId);
            //}

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