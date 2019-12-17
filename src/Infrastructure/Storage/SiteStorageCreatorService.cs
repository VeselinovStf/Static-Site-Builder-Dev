using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Interfaces;
using Infrastructure.BuildInOptions;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.HostingHubConnectorService;
using Infrastructure.Services.HostingHubConnectorService.DTOs;
using Infrastructure.Services.RepoHubConnectorService;
using Infrastructure.Storage.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Storage
{
    public class SiteStorageCreatorService : ISiteStorageCreatorService
    {
        public AuthHostingConnectorOptions HostingOptions { get; }
        public AuthRepoHubConnectorOptions RepoOptions { get; }
        public TemplatesRepoOptions TemplatesRepoOptions { get; }

        private readonly IOptions<TemplatesRepoOptions> templateRepoOptions;
        private readonly IHubKeyMaker<HostingCreatePrepDTO> hostingHubDeployKeyMaker;
        private readonly IRepoHubConnector<RepoPullTemplateDTO> repoHubConnector;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService;
        private readonly IRepoHubKeyMaker repoHubKeyMaker;
        private readonly IHostingHubConnector hostingHubConnector;

        public SiteStorageCreatorService(
            IOptions<AuthHostingConnectorOptions> hostingOptions,
            IOptions<AuthRepoHubConnectorOptions> repoOptions,
            IOptions<TemplatesRepoOptions> templateRepoOptions,
            IHubKeyMaker<HostingCreatePrepDTO> hostingHubDeployKeyMaker,
            IRepoHubConnector<RepoPullTemplateDTO> repoHubConnector,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService,
            IRepoHubKeyMaker repoHubKeyMaker,
            IHostingHubConnector hostingHubConnector)
        {
            this.HostingOptions = hostingOptions.Value;
            this.RepoOptions = repoOptions.Value;
            this.TemplatesRepoOptions = templateRepoOptions.Value;
            this.hostingHubDeployKeyMaker = hostingHubDeployKeyMaker ?? throw new ArgumentNullException(nameof(hostingHubDeployKeyMaker));
            this.repoHubConnector = repoHubConnector ?? throw new ArgumentNullException(nameof(repoHubConnector));
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
            this.repoHubKeyMaker = repoHubKeyMaker ?? throw new ArgumentNullException(nameof(repoHubKeyMaker));
            this.hostingHubConnector = hostingHubConnector ?? throw new ArgumentNullException(nameof(hostingHubConnector));
        }

        public async Task<bool> StorageCreatorExecute(string newRepositoryCreateName, string templateName)
        {
            try
            {
                Validator.StringIsNullOrEmpty(
                    newRepositoryCreateName, $"{nameof(SiteStorageCreatorService)} : {nameof(StorageCreatorExecute)} : {nameof(newRepositoryCreateName)} : is null/empty");

                Validator.StringIsNullOrEmpty(
                    templateName, $"{nameof(SiteStorageCreatorService)} : {nameof(StorageCreatorExecute)} : {nameof(templateName)} : is null/empty");

                var hostingDeployKey = await this.hostingHubDeployKeyMaker.CreateKey(this.HostingOptions.HostAccesToken);

                Validator.ObjectIsNull(
                   hostingDeployKey, $"{nameof(SiteStorageCreatorService)} : {nameof(StorageCreatorExecute)} : {nameof(hostingDeployKey)} : Hosting deploy key is null");

                var createdRepoHubId = await this.repoHubConnector.CreateHub(newRepositoryCreateName, RepoOptions.RepoAccesTokken);

                Validator.StringIsNullOrEmpty(
                    createdRepoHubId, $"{nameof(SiteStorageCreatorService)} : {nameof(StorageCreatorExecute)} : {nameof(createdRepoHubId)} : Created repo hub id is null!");

                var repoUserKey = await this.repoHubKeyMaker.CreateKey(this.RepoOptions.RepoAccesTokken, hostingDeployKey.PublicKey, newRepositoryCreateName);

                var pushToRepo = await this.repoHubConnector.PushProject(createdRepoHubId, templateName, RepoOptions.RepoAccesTokken);

                if (!pushToRepo)
                {
                    throw new SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception($"{nameof(SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception)} : Error : Can't push to repository!!");
                }

                var deployCall = await this.hostingHubConnector.CreateHub(newRepositoryCreateName, newRepositoryCreateName, createdRepoHubId, hostingDeployKey.Id, HostingOptions.HostAccesToken);

                Validator.StringIsNullOrEmpty(
                   deployCall, $"{nameof(SiteStorageCreatorService)} : {nameof(StorageCreatorExecute)} : {nameof(deployCall)} : Can't deploy site to hosting!");

                return true;
            }
            catch (Exception ex)
            {
                throw new SiteStorageCreatorService_StorageCreatorExecute_Exception($"{nameof(SiteStorageCreatorService_StorageCreatorExecute_Exception)} : Exception : Can't add site to storage!! : {ex.Message}");
            }
        }

        public async Task UpdateTemplate(string templateName)
        {
            ////call api and get elements
            RepoPullTemplateDTO elements = await this.repoHubConnector.PullDataFromHub(TemplatesRepoOptions.TemplatesRepositoryHubId, templateName,RepoOptions.RepoAccesTokken);
            ////convert if is neaded
            await this.appSiteTemplateService.AddTemplateElementsAsync(templateName, elements);
            ////add them to Db
        }
    }
}