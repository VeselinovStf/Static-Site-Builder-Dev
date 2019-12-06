using ApplicationCore.Entities;
using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.SiteTypes.DTOs;
using Infrastructure.SiteTypes.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.SiteTypes
{
    public class SiteTypeEditorService : ISiteTypeEditorService<SiteTypeEditorDTO>
    {
        private readonly IAppProjectsService<Project> appProjectService;
        private readonly IAppStoreTypeSiteService<StoreTypeSite> appStoreTypeService;
        private readonly IAppBlogTypeSiteService<BlogTypeSite> appBlogTypeService;
        private readonly IAppLaunchConfigService<LaunchConfig> appLaunchConfigService;

        public SiteTypeEditorService(
            IAppProjectsService<Project> appProjectService,
            IAppStoreTypeSiteService<StoreTypeSite> appStoreTypeService,
            IAppBlogTypeSiteService<BlogTypeSite> appBlogTypeService,
            IAppLaunchConfigService<LaunchConfig> appLaunchConfigService)
        {
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.appStoreTypeService = appStoreTypeService ?? throw new ArgumentNullException(nameof(appStoreTypeService));
            this.appBlogTypeService = appBlogTypeService ?? throw new ArgumentNullException(nameof(appBlogTypeService));
            this.appLaunchConfigService = appLaunchConfigService ?? throw new ArgumentNullException(nameof(appLaunchConfigService));
        }

        public async Task DeleteSiteTypeAsync(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteTypeEditorService)} : {nameof(DeleteSiteTypeAsync)} : {nameof(clientId)} : is null/empty");
            Validator.StringIsNullOrEmpty(
              siteTypeId, $"{nameof(SiteTypeEditorService)} : {nameof(DeleteSiteTypeAsync)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var clientProjects = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjects, $"{nameof(SiteTypeEditorService)} : {nameof(DeleteSiteTypeAsync)} : {nameof(clientProjects)} : Can't find client project types!");

                var resultModel = new SiteTypeEditorDTO();

                // Find in any pre-build siteType
                var clientBlogSiteType = clientProjects.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreSiteType = clientProjects.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                if (clientBlogSiteType == null)
                {
                    if (clientStoreSiteType == null)
                    {
                        throw new SiteTypeEditorDeleteSiteTypeNoSiteTypeException($"{nameof(SiteTypeEditorDeleteSiteTypeNoSiteTypeException)} : ATTENTION: UNLEAGLE ACTION: Client : {clientId} : doesn't have any sites created!");
                    }
                    else
                    {
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);

                        if (!clientStoreConfig.IsLaunched)
                        {
                            await this.appStoreTypeService.DeleteClientStoreProjectAsync(clientId);
                        }
                        else
                        {
                            throw new SiteTypeEditorDeleteSiteTypeIsLaunchedException($"{nameof(SiteTypeEditorDeleteSiteTypeIsLaunchedException)} : Can't delete launched site type!");
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);

                    if (!clientBlogConfig.IsLaunched)
                    {
                        await this.appBlogTypeService.DeleteClientBlogProjectAsync(clientId);
                    }
                    else
                    {
                        throw new SiteTypeEditorDeleteSiteTypeIsLaunchedException($"{nameof(SiteTypeEditorDeleteSiteTypeIsLaunchedException)} : Can't delete launched site type!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SiteTypeEditorDeleteSiteTypeException($"{nameof(SiteTypeEditorDeleteSiteTypeException)} : Can't delete client site type! : {ex.Message}");
            }
        }

        public async Task EditSiteTypeAsync(string name, string description, string clientId, string id, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository)
        {
            Validator.StringIsNullOrEmpty(
                name, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(name)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                description, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(description)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                cardApiKey, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(cardApiKey)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                cardServiceGate, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(cardServiceGate)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                hostingServiceGate, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(hostingServiceGate)} : is null/empty");
            Validator.StringIsNullOrEmpty(
                repository, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(repository)} : is null/empty");

            try
            {
                var clientProjects = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjects, $"{nameof(SiteTypeEditorService)} : {nameof(EditSiteTypeAsync)} : {nameof(clientProjects)} : Can't find client project types!");

                var resultModel = new SiteTypeEditorDTO();

                // Find in any pre-build siteType
                var clientBlogSiteType = clientProjects.BlogSiteTypes.FirstOrDefault(b => b.Id == id);
                var clientStoreSiteType = clientProjects.StoreSiteTypes.FirstOrDefault(b => b.Id == id);

                if (clientBlogSiteType == null)
                {
                    if (clientStoreSiteType == null)
                    {
                        throw new SiteTypeEditorEditSiteTypeNoSiteTypeException($"{nameof(SiteTypeEditorEditSiteTypeNoSiteTypeException)} : ATTENTION: UNLEAGLE ACTION: Client : {clientId} : doesn't have any sites created!");
                    }
                    else
                    {
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);

                        if (!clientStoreConfig.IsLaunched)
                        {
                            await this.appStoreTypeService.EditClientStoreProjectAsync(clientId, name, description, cardApiKey, cardServiceGate, hostingServiceGate, repository);
                        }
                        else
                        {
                            throw new SiteTypeEditorEditSiteTypeIsLaunchedException($"{nameof(SiteTypeEditorEditSiteTypeIsLaunchedException)} : Can't edit launched site type!");
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);

                    if (!clientBlogConfig.IsLaunched)
                    {
                        await this.appBlogTypeService.EditClientBlogProjectAsync(clientId, name, description, cardApiKey, cardServiceGate, hostingServiceGate, repository);
                    }
                    else
                    {
                        throw new SiteTypeEditorEditSiteTypeIsLaunchedException($"{nameof(SiteTypeEditorEditSiteTypeIsLaunchedException)} : Can't edit launched site type!");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new SiteTypeEditorEditSiteTypeException($"{nameof(SiteTypeEditorEditSiteTypeException)} : Can't get client site type! : {ex.Message}");
            }
        }

        public async Task<SiteTypeEditorDTO> GetClientEditableSiteTypeAsync(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteTypeEditorService)} : {nameof(GetClientEditableSiteTypeAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               siteTypeId, $"{nameof(SiteTypeEditorService)} : {nameof(GetClientEditableSiteTypeAsync)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var clientProjects = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjects, $"{nameof(SiteTypeEditorService)} : {nameof(GetClientEditableSiteTypeAsync)} : {nameof(clientProjects)} : Can't find client project types!");

                var resultModel = new SiteTypeEditorDTO();

                // Find in any pre-build siteType
                var clientBlogSiteType = clientProjects.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreSiteType = clientProjects.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                if (clientBlogSiteType == null)
                {
                    if (clientStoreSiteType == null)
                    {
                        throw new SiteTypeEditorGetClientEditableSiteTypeNoSiteTypeException($"{nameof(SiteTypeEditorGetClientEditableSiteTypeNoSiteTypeException)} : ATTENTION: UNLEAGLE ACTION: Client : {clientId} : doesn't have any sites created!");
                    }
                    else
                    {
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);

                        if (!clientStoreConfig.IsLaunched)
                        {
                            resultModel.Id = clientStoreSiteType.Id;
                            resultModel.CardApiKey = clientStoreConfig.CardApiKey;
                            resultModel.CardServiceGate = clientStoreConfig.CardServiceGate;
                            resultModel.HostingServiceGate = clientStoreConfig.HostingServiceGate;
                            resultModel.Description = clientStoreSiteType.Description;
                            resultModel.Name = clientStoreSiteType.Name;

                            resultModel.Repository = clientStoreConfig.RepositoryId;
                            resultModel.TemplateLocation = clientStoreSiteType.TemplateName;
                            resultModel.ClientId = clientStoreSiteType.ClientId;
                            resultModel.IsLaunched = clientStoreConfig.IsLaunched;

                            return resultModel;
                        }
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);

                    if (!clientBlogConfig.IsLaunched)
                    {
                        resultModel.Id = clientBlogSiteType.Id;
                        resultModel.CardApiKey = clientBlogConfig.CardApiKey;
                        resultModel.CardServiceGate = clientBlogConfig.CardServiceGate;
                        resultModel.HostingServiceGate = clientBlogConfig.HostingServiceGate;
                        resultModel.Description = clientBlogSiteType.Description;
                        resultModel.Name = clientBlogSiteType.Name;

                        resultModel.Repository = clientBlogConfig.RepositoryId;
                        resultModel.TemplateLocation = clientBlogSiteType.TemplateName;
                        resultModel.ClientId = clientBlogSiteType.ClientId;
                        resultModel.IsLaunched = clientBlogConfig.IsLaunched;

                        return resultModel;
                    }
                }

                throw new SiteTypeEditorGetClientEditableSiteTypeIsLaunchedException($"{nameof(SiteTypeEditorGetClientEditableSiteTypeIsLaunchedException)} : Can't get edit launched site type!");
            }
            catch (Exception ex)
            {
                throw new SiteTypeEditorGetClientEditableSiteTypeException($"{nameof(SiteTypeEditorGetClientEditableSiteTypeException)} : Can't get client site type! : {ex.Message}");
            }
        }

        public async Task<SiteTypeEditorDTO> GetClientSiteTypeAsync(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteTypeEditorService)} : {nameof(GetClientEditableSiteTypeAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               siteTypeId, $"{nameof(SiteTypeEditorService)} : {nameof(GetClientEditableSiteTypeAsync)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var clientProjects = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjects, $"{nameof(SiteTypeEditorService)} : {nameof(GetClientEditableSiteTypeAsync)} : {nameof(clientProjects)} : Can't find client project types!");

                var resultModel = new SiteTypeEditorDTO();

                // Find in any pre-build siteType
                var clientBlogSiteType = clientProjects.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreSiteType = clientProjects.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                if (clientBlogSiteType == null)
                {
                    if (clientStoreSiteType == null)
                    {
                        throw new SiteTypeEditorGetClientEditableSiteTypeNoSiteTypeException($"{nameof(SiteTypeEditorGetClientEditableSiteTypeNoSiteTypeException)} : ATTENTION: UNLEAGLE ACTION: Client : {clientId} : doesn't have any sites created!");
                    }
                    else
                    {
                        var clientStoreConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientStoreSiteType.Id);

                        resultModel.Id = clientStoreSiteType.Id;
                        resultModel.CardApiKey = clientStoreConfig.CardApiKey;
                        resultModel.CardServiceGate = clientStoreConfig.CardServiceGate;
                        resultModel.HostingServiceGate = clientStoreConfig.HostingServiceGate;
                        resultModel.Description = clientStoreSiteType.Description;
                        resultModel.Name = clientStoreSiteType.Name;
                        resultModel.TemplateName = clientStoreSiteType.TemplateName;
                        resultModel.Repository = clientStoreConfig.RepositoryId;
                        resultModel.TemplateLocation = clientStoreSiteType.TemplateName;
                        resultModel.ClientId = clientStoreSiteType.ClientId;
                        resultModel.IsLaunched = clientStoreConfig.IsLaunched;

                        return resultModel;
                    }
                }
                else
                {
                    var clientBlogConfig = await this.appLaunchConfigService.GetSiteTypeLaunchConfigAsync(clientBlogSiteType.Id);

                    resultModel.Id = clientBlogSiteType.Id;
                    resultModel.CardApiKey = clientBlogConfig.CardApiKey;
                    resultModel.CardServiceGate = clientBlogConfig.CardServiceGate;
                    resultModel.HostingServiceGate = clientBlogConfig.HostingServiceGate;
                    resultModel.Description = clientBlogSiteType.Description;
                    resultModel.Name = clientBlogSiteType.Name;
                    resultModel.TemplateName = clientStoreSiteType.TemplateName;
                    resultModel.Repository = clientBlogConfig.RepositoryId;
                    resultModel.TemplateLocation = clientBlogSiteType.TemplateName;
                    resultModel.ClientId = clientBlogSiteType.ClientId;
                    resultModel.IsLaunched = clientBlogConfig.IsLaunched;

                    return resultModel;
                }

                throw new SiteTypeEditorGetClientEditableSiteTypeIsLaunchedException($"{nameof(SiteTypeEditorGetClientEditableSiteTypeIsLaunchedException)} : Can't get edit launched site type!");
            }
            catch (Exception ex)
            {
                throw new SiteTypeEditorGetClientEditableSiteTypeException($"{nameof(SiteTypeEditorGetClientEditableSiteTypeException)} : Can't get client site type! : {ex.Message}");
            }
        }
    }
}