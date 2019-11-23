using ApplicationCore.Entities;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.LaunchSite.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.LaunchSite
{
    public class LaunchSiteService : ILaunchSiteService
    {
        private readonly IAppProjectsService<Project> appProjectService;
        private readonly IHubConnector fileTransporter;
        private readonly IAppLaunchConfigService<LaunchConfig> appLaunchConfigService;

        public LaunchSiteService(
           IAppProjectsService<Project> appProjectService,
           IHubConnector fileTransporter,
           IAppLaunchConfigService<LaunchConfig> appLaunchConfigService)
        {
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.fileTransporter = fileTransporter ?? throw new ArgumentNullException(nameof(fileTransporter));
            this.appLaunchConfigService = appLaunchConfigService ?? throw new ArgumentNullException(nameof(appLaunchConfigService));
        }

        public async Task LaunchSite(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(LaunchSiteService)} : {nameof(LaunchSite)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                siteTypeId, $"{nameof(LaunchSiteService)} : {nameof(LaunchSite)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var clientProjects = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjects, $"{nameof(LaunchSiteService)} : {nameof(LaunchSite)} : {nameof(clientProjects)} : Can't find client project types!");

                // Find in any pre-build siteType
                var clientBlogSiteType = clientProjects.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreSiteType = clientProjects.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                if (clientBlogSiteType == null)
                {
                    if (clientStoreSiteType == null)
                    {
                        throw new LaunchSiteServiceSiteTypeNoSiteTypeException($"{nameof(LaunchSiteServiceSiteTypeNoSiteTypeException)} : ATTENTION: UNLEAGLE ACTION: Client : {clientId} : doesn't have any sites created!");
                    }
                    else
                    {
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfig(clientStoreSiteType.Id);

                        if (!clientStoreConfig.IsLaunched && !clientStoreConfig.IsPushed)
                        {
                            await this.fileTransporter.CreateHub("TestSSB");
                            this.fileTransporter.DirectoryCoppy(clientStoreSiteType.TemplateLocation, clientStoreSiteType.NewProjectLocation);

                            await this.appLaunchConfigService.LaunchSiteTypeLaunchConfig(clientStoreSiteType.Id);
                            await this.appLaunchConfigService.PushSiteTypeLaunchConfig(clientStoreSiteType.Id);
                        }
                        else if (clientStoreConfig.IsPushed)
                        {
                            //Re-Launch
                            await this.appLaunchConfigService.LaunchSiteTypeLaunchConfig(clientStoreSiteType.Id);
                        }
                        else
                        {
                            throw new LaunchSiteServiceSiteTypeIsLaunchedException($"{nameof(LaunchSiteServiceSiteTypeIsLaunchedException)} : Can't launch site type!");
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfig(clientBlogSiteType.Id);

                    if (!clientBlogConfig.IsLaunched && !clientBlogConfig.IsPushed)
                    {
                        this.fileTransporter.DirectoryCoppy(clientBlogSiteType.TemplateLocation, clientBlogSiteType.NewProjectLocation);

                        await this.appLaunchConfigService.LaunchSiteTypeLaunchConfig(clientBlogSiteType.Id);
                        await this.appLaunchConfigService.PushSiteTypeLaunchConfig(clientBlogSiteType.Id);
                    }
                    else if (clientBlogConfig.IsPushed)
                    {
                        //Re-Launch
                        await this.appLaunchConfigService.LaunchSiteTypeLaunchConfig(clientBlogSiteType.Id);
                    }
                    else
                    {
                        throw new LaunchSiteServiceSiteTypeIsLaunchedException($"{nameof(LaunchSiteServiceSiteTypeIsLaunchedException)} : Can't launch site type!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new LaunchSiteServiceLaunchSiteException($"{nameof(LaunchSiteServiceLaunchSiteException)} : Can't launch site! : {ex.Message}");
            }
        }

        public async Task UnLaunchSite(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(LaunchSiteService)} : {nameof(LaunchSite)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                siteTypeId, $"{nameof(LaunchSiteService)} : {nameof(LaunchSite)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var clientProjects = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjects, $"{nameof(LaunchSiteService)} : {nameof(LaunchSite)} : {nameof(clientProjects)} : Can't find client project types!");

                // Find in any pre-build siteType
                var clientBlogSiteType = clientProjects.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreSiteType = clientProjects.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                if (clientBlogSiteType == null)
                {
                    if (clientStoreSiteType == null)
                    {
                        throw new UnLaunchSiteServiceSiteTypeNoSiteTypeException($"{nameof(UnLaunchSiteServiceSiteTypeNoSiteTypeException)} : ATTENTION: UNLEAGLE ACTION: Client : {clientId} : doesn't have any sites created!");
                    }
                    else
                    {
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfig(clientStoreSiteType.Id);

                        if (clientStoreConfig.IsLaunched)
                        {
                            //this.fileTransporter.WholeDirectoryTransport()

                            await this.appLaunchConfigService.UnLaunchSiteTypeLaunchConfig(clientStoreSiteType.Id);
                        }
                        else
                        {
                            throw new UnLaunchSiteServiceSiteTypeIsNotLaunchedException($"{nameof(UnLaunchSiteServiceSiteTypeIsNotLaunchedException)} : Can't un-launch site type!");
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfig(clientBlogSiteType.Id);

                    if (clientBlogConfig.IsLaunched)
                    {
                        //this.fileTransporter.WholeDirectoryTransport()

                        await this.appLaunchConfigService.UnLaunchSiteTypeLaunchConfig(clientBlogSiteType.Id);
                    }
                    else
                    {
                        throw new UnLaunchSiteServiceSiteTypeIsNotLaunchedException($"{nameof(UnLaunchSiteServiceSiteTypeIsNotLaunchedException)} : Can't un-launch site type!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new UnLaunchSiteServiceLaunchSiteException($"{nameof(LaunchSiteServiceLaunchSiteException)} : Can't un-launch site! : {ex.Message}");
            }
        }
    }
}