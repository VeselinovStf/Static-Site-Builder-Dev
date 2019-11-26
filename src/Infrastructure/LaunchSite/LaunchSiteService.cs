using ApplicationCore.Entities;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.LaunchSite.Exceptions;
using Infrastructure.Templates.DTOs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.LaunchSite
{
    public class LaunchSiteService : ILaunchSiteService
    {
        private readonly IAppProjectsService<Project> appProjectService;
        private readonly IRepoHubConnector repoHubConnectorAPI;
        private readonly IHostingHubConnector hostingHubConnectorAPI;
        private readonly IHubConnectorRepoOption repoOptionsAPI;
        private readonly ITemplateService<SiteTemplateDTO> templateService;
        private readonly IAppLaunchConfigService<LaunchConfig> appLaunchConfigService;

        public LaunchSiteService(
           IAppProjectsService<Project> appProjectService,
           IRepoHubConnector repoHubConnectorAPI,
           IHostingHubConnector hostingHubConnectorAPI,
           IHubConnectorRepoOption repoOptionsAPI,
           ITemplateService<SiteTemplateDTO> templateService,
           IAppLaunchConfigService<LaunchConfig> appLaunchConfigService)
        {
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.repoHubConnectorAPI = repoHubConnectorAPI ?? throw new ArgumentNullException(nameof(repoHubConnectorAPI));
            this.hostingHubConnectorAPI = hostingHubConnectorAPI ?? throw new ArgumentNullException(nameof(hostingHubConnectorAPI));
            this.repoOptionsAPI = repoOptionsAPI ?? throw new ArgumentNullException(nameof(repoOptionsAPI));
            this.templateService = templateService ?? throw new ArgumentNullException(nameof(templateService));
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
                            var clientTemplateName = clientStoreSiteType.TemplateName;
                            var clientBuildInSiteType = clientStoreSiteType.SiteTypeSpecification.ToString();

                            //Create Repository Hub
                            var repoHubId = await this.repoHubConnectorAPI.CreateHub(clientProjectName);
                            //Save to db Repo Hub Id
                            await this.appLaunchConfigService.AddRepositoryIdAsync(clientStoreSiteType.Id, repoHubId);

                            //Create Hosting Project
                            var hostHubId = await this.hostingHubConnectorAPI.CreateHub(clientProjectName);
                            //Save Hosting created project id to db
                            await this.appLaunchConfigService.AddHostingIdAsync(clientStoreSiteType.Id, hostHubId);

                            //Add CiCd variables to Repo Hub
                            await this.repoOptionsAPI.AddCiCDVariables(repoHubId, hostHubId);
                            //Add Variables to Db Template
                            //           await this.templateService.ConfigureCiCdVariables(hostHubId, clientBuildInSiteType, clientTemplateName);

                            //Push Project template do Repository
                            await this.repoHubConnectorAPI.PushProject(repoHubId, clientStoreSiteType.TemplateName);

                            //Mark IsLanched
                            await this.appLaunchConfigService.LaunchSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);
                            //Mark IsPushed
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

                        var createdHubId = await this.repoHubConnectorAPI.CreateHub(clientProjectName);

                        await this.appLaunchConfigService.AddRepositoryIdAsync(clientBlogSiteType.Id, createdHubId);

                        await this.repoHubConnectorAPI.PushProject(createdHubId, clientBlogSiteType.TemplateName);

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