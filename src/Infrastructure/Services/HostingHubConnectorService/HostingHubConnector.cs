using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.HostingHubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.HostingHubConnectorService
{
    public class HostingHubConnector : IHostingHubConnector
    {
        private readonly IAPIHostClientService<NetlifyHubClient> hubClient;

        public HostingHubConnector(
            IOptions<AuthHostingConnectorOptions> options,
            IAPIHostClientService<NetlifyHubClient> hubClient
            )
        {
            this.Options = options.Value;
            this.hubClient = hubClient ?? throw new ArgumentNullException(nameof(hubClient));
        }

        public AuthHostingConnectorOptions Options { get; }

        public async Task<string> CreateHub(string name, string deployKeyId)
        {
            return await ExecuteCreate(name, Options.HostAccesToken);
        }

        private async Task<string> ExecuteCreate(string name, string hostAccesToken)
        {
            Validator.StringIsNullOrEmpty(
             name, $"{nameof(HostingHubConnector)} : {nameof(ExecuteCreate)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              hostAccesToken, $"{nameof(HostingHubConnector)} : {nameof(ExecuteCreate)} : {nameof(hostAccesToken)} : is null/empty");

            try
            {
                var clientHubCallId = await this.hubClient.CreateHubAsync(name, hostAccesToken);

                Validator.StringIsNullOrEmpty(
                        clientHubCallId, $"{nameof(HostingHubConnector)} : {nameof(ExecuteCreate)} : {nameof(clientHubCallId)} : is null/empty");

                return clientHubCallId;
            }
            catch (Exception ex)
            {
                throw new HostingHubConnectorCreateHubException($"{nameof(HostingHubConnectorCreateHubException)} : Exception : Can't create hosting hub! : {ex.Message}");
            }
        }
    }
}