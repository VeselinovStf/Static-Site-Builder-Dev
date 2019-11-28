using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.HostingHubConnectorService.Exceptions;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.HostingHubConnectorService
{
    public class HostingHubConnector : IHostingHubConnector
    {
        private readonly IAPIHostClientService<NetlifyHubClient> hubClient;

        public HostingHubConnector(

            IAPIHostClientService<NetlifyHubClient> hubClient
            )
        {
            this.hubClient = hubClient ?? throw new ArgumentNullException(nameof(hubClient));
        }

        public async Task<string> CreateHub(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken)
        {
            Validator.StringIsNullOrEmpty(
             netlifySiteName, $"{nameof(HostingHubConnector)} : {nameof(CreateHub)} : {nameof(netlifySiteName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              deployKeyId, $"{nameof(HostingHubConnector)} : {nameof(CreateHub)} : {nameof(deployKeyId)} : is null/empty");

            try
            {
                var clientHubCallId = await this.hubClient.CreateHubAsync(netlifySiteName, repositoryName, repositoryId, deployKeyId, accesToken);

                Validator.StringIsNullOrEmpty(
                        clientHubCallId, $"{nameof(HostingHubConnector)} : {nameof(CreateHub)} : {nameof(clientHubCallId)} : is null/empty");

                return clientHubCallId;
            }
            catch (Exception ex)
            {
                throw new HostingHubConnectorCreateHubException($"{nameof(HostingHubConnectorCreateHubException)} : Exception : Can't create hosting hub! : {ex.Message}");
            }
        }
    }
}