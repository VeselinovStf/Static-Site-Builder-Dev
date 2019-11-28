using ApplicationCore.Interfaces;
using Infrastructure.Guard;
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

        private readonly IHubKeyMaker<HostingCreatePrepDTO> hostingHubDeployKeyMaker;
        private readonly IRepoHubConnector repositoryHubConnector;
        private readonly IRepoHubKeyMaker repoHubKeyMaker;
        private readonly IHostingHubConnector hostingHubConnector;

        public SiteStorageCreatorService(
            IOptions<AuthHostingConnectorOptions> hostingOptions,
            IOptions<AuthRepoHubConnectorOptions> repoOptions,
            IHubKeyMaker<HostingCreatePrepDTO> hostingHubDeployKeyMaker,
            IRepoHubConnector repositoryHubConnector,
            IRepoHubKeyMaker repoHubKeyMaker,
            IHostingHubConnector hostingHubConnector)
        {
            this.HostingOptions = hostingOptions.Value;
            this.RepoOptions = repoOptions.Value;
            this.hostingHubDeployKeyMaker = hostingHubDeployKeyMaker ?? throw new ArgumentNullException(nameof(hostingHubDeployKeyMaker));
            this.repositoryHubConnector = repositoryHubConnector ?? throw new ArgumentNullException(nameof(repositoryHubConnector));
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

                var createdRepoHubId = await this.repositoryHubConnector.CreateHub(newRepositoryCreateName);

                Validator.StringIsNullOrEmpty(
                    createdRepoHubId, $"{nameof(SiteStorageCreatorService)} : {nameof(StorageCreatorExecute)} : {nameof(createdRepoHubId)} : Created repo hub id is null!");

                var repoUserKey = await this.repoHubKeyMaker.CreateKey(this.RepoOptions.AccesTokken, hostingDeployKey.PublicKey, newRepositoryCreateName);

                var pushToRepo = await this.repositoryHubConnector.PushProject(createdRepoHubId, templateName);

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
    }
}