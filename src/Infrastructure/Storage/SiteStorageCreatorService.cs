using ApplicationCore.Interfaces;
using Infrastructure.Services.HostingHubConnectorService.DTOs;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Storage
{
    public class SiteStorageCreatorService : ISiteStorageCreatorService
    {
        private readonly IHubKeyMaker<HostingCreatePrepDTO> hostingHubDeployKeyMaker;
        private readonly IRepoHubConnector repositoryHubConnector;
        private readonly IRepoHubKeyMaker repoHubKeyMaker;
        private readonly IHostingHubConnector hostingHubConnector;

        public SiteStorageCreatorService(
            IHubKeyMaker<HostingCreatePrepDTO> hostingHubDeployKeyMaker,
            IRepoHubConnector repositoryHubConnector,
            IRepoHubKeyMaker repoHubKeyMaker,
            IHostingHubConnector hostingHubConnector)
        {
            this.hostingHubDeployKeyMaker = hostingHubDeployKeyMaker ?? throw new ArgumentNullException(nameof(hostingHubDeployKeyMaker));
            this.repositoryHubConnector = repositoryHubConnector ?? throw new ArgumentNullException(nameof(repositoryHubConnector));
            this.repoHubKeyMaker = repoHubKeyMaker ?? throw new ArgumentNullException(nameof(repoHubKeyMaker));
            this.hostingHubConnector = hostingHubConnector ?? throw new ArgumentNullException(nameof(hostingHubConnector));
        }

        public async Task<bool> StorageCreatorExecute()
        {
            //var hostingKey = await this.hostingHubDeployKeyMaker.CreateKey();

            //var createdHub = await this.repositoryHubConnector.CreateHub();

            //var repoUserKey = await this.repoHubKeyMaker.CreateKey();

            //var pushToRepo = await this.repositoryHubConnector.PushProject();

            //var deployCall = await this.hostingHubConnector.CreateHub();

            return false;
        }
    }
}