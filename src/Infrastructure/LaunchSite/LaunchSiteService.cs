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
        private readonly IHubConnector repoHubConnector;
        private readonly IHubConnector hostingHubConnector;
        private readonly IHubConnectorRepoOption repoOptions;
        private readonly IAppLaunchConfigService<LaunchConfig> appLaunchConfigService;

        public LaunchSiteService(
           IAppProjectsService<Project> appProjectService,
           IHubConnector repoHubConnector,
           IHubConnector hostingHubConnector,
           IHubConnectorRepoOption repoOptions,
           IAppLaunchConfigService<LaunchConfig> appLaunchConfigService)
        {
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.repoHubConnector = repoHubConnector ?? throw new ArgumentNullException(nameof(repoHubConnector));
            this.hostingHubConnector = hostingHubConnector ?? throw new ArgumentNullException(nameof(hostingHubConnector));
            this.repoOptions = repoOptions ?? throw new ArgumentNullException(nameof(repoOptions));
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
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);

                        if (!clientStoreConfig.IsLaunched && !clientStoreConfig.IsPushed)
                        {
                            var clientProjectName = clientStoreConfig.RepositoryName;

                            var repoHubId = await this.repoHubConnector.CreateHub(clientProjectName);
                            await this.appLaunchConfigService.AddRepositoryIdAsync(clientStoreSiteType.Id, repoHubId);

                            var hostHubId = await this.hostingHubConnector.CreateHub(clientProjectName);
                            await this.appLaunchConfigService.AddHostingIdAsync(clientStoreSiteType.Id, hostHubId);

                            await this.repoOptions.AddCiCDVariables(repoHubId, hostHubId);
                            //TODO: A1- ADD -> Variables to gitlab-ci.yml
                            //TODO: A1? - Where are stored all templates??

                            await this.repoHubConnector.PushProject(repoHubId, clientStoreSiteType.TemplateLocation);

                            await this.appLaunchConfigService.LaunchSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);
                            await this.appLaunchConfigService.PushSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);
                        }
                        else if (clientStoreConfig.IsPushed)
                        {
                            //Re-Launch
                            await this.appLaunchConfigService.LaunchSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);
                        }
                        else
                        {
                            throw new LaunchSiteServiceSiteTypeIsLaunchedException($"{nameof(LaunchSiteServiceSiteTypeIsLaunchedException)} : Can't launch site type!");
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);

                    if (!clientBlogConfig.IsLaunched && !clientBlogConfig.IsPushed)
                    {
                        var clientProjectName = clientBlogConfig.RepositoryName;

                        var createdHubId = await this.repoHubConnector.CreateHub(clientProjectName);

                        await this.appLaunchConfigService.AddRepositoryIdAsync(clientBlogSiteType.Id, createdHubId);

                        await this.repoHubConnector.PushProject(createdHubId, clientBlogSiteType.TemplateLocation);

                        await this.appLaunchConfigService.LaunchSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);
                        await this.appLaunchConfigService.PushSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);
                    }
                    else if (clientBlogConfig.IsPushed)
                    {
                        //Re-Launch
                        await this.appLaunchConfigService.LaunchSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);
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
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);

                        if (clientStoreConfig.IsLaunched)
                        {
                            //this.fileTransporter.WholeDirectoryTransport()

                            await this.appLaunchConfigService.UnLaunchSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);
                        }
                        else
                        {
                            throw new UnLaunchSiteServiceSiteTypeIsNotLaunchedException($"{nameof(UnLaunchSiteServiceSiteTypeIsNotLaunchedException)} : Can't un-launch site type!");
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);

                    if (clientBlogConfig.IsLaunched)
                    {
                        //this.fileTransporter.WholeDirectoryTransport()

                        await this.appLaunchConfigService.UnLaunchSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);
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