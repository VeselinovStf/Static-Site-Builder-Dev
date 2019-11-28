using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.HostingHubConnectorService.DTOs;
using Infrastructure.Services.HostingHubConnectorService.Exceptions;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.HostingHubConnectorService
{
    public class HostingHubKeyMaker : IHubKeyMaker<HostingCreatePrepDTO>
    {
        private readonly IHostDeployToken<DeployKeyDTO> hostingDeployKeyMaker;

        public HostingHubKeyMaker(IHostDeployToken<DeployKeyDTO> hostingDeployKeyMaker)
        {
            this.hostingDeployKeyMaker = hostingDeployKeyMaker ?? throw new ArgumentNullException(nameof(hostingDeployKeyMaker));
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