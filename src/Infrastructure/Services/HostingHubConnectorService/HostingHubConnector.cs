using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.HostingHubConnectorService.DTOs;
using Infrastructure.Services.HostingHubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.HostingHubConnectorService
{
    public class HostingHubConnector : IHostingHubConnector, IHostingHubKeyMaker<HostingCreatePrepDTO>
    {
        private readonly IAPIHostClientService<NetlifyHubClient> hubClient;
        private readonly IHostDeployToken<DeployKeyDTO> hostingDeployKeyMaker;

        public HostingHubConnector(
            IOptions<AuthHostingConnectorOptions> options,
            IAPIHostClientService<NetlifyHubClient> hubClient,
            IHostDeployToken<DeployKeyDTO> hostingDeployKeyMaker)
        {
            this.Options = options.Value;
            this.hubClient = hubClient ?? throw new ArgumentNullException(nameof(hubClient));
            this.hostingDeployKeyMaker = hostingDeployKeyMaker ?? throw new ArgumentNullException(nameof(hostingDeployKeyMaker));
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

        public async Task<HostingCreatePrepDTO> CreateKey(string hostAccesToken)
        {
            Validator.StringIsNullOrEmpty(
             hostAccesToken, $"{nameof(HostingHubConnector)} : {nameof(CreateKey)} : {nameof(hostAccesToken)} : is null/empty");

            try
            {
                var keyMakerCall = await this.hostingDeployKeyMaker.CreateDeployKey(hostAccesToken);

                Validator.ObjectIsNull(
                        keyMakerCall, $"{nameof(HostingHubConnector)} : {nameof(CreateKey)} : {nameof(keyMakerCall)} : deploy keys are null");

                var returnModel = new HostingCreatePrepDTO()
                {
                    Id = keyMakerCall.Id,
                    PublicKey = keyMakerCall.PublicKey
                };

                return returnModel;
            }
            catch (Exception ex)
            {
                throw new HostingHubConnectorPrepareForHubCreationException($"{nameof(HostingHubConnectorPrepareForHubCreationException)} : Exception : Can't prepare for hosting hub creation! : {ex.Message}");
            }
        }
    }
}