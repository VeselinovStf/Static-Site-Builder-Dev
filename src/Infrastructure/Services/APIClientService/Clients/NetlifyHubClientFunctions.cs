using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.APIClientService.Exceptions;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class NetlifyHubClient
    {
        public async Task<DeployKeyDTO> DeployKeys(string accesToken)
        {
            var response = await this.Client.PostAsync($"deploy_keys?access_token={accesToken}", base.CreateHttpContent<string>(""));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = GetCreatedResponse<DeployKeyDTO>(responseBody);

                return resultIdModel;
            }

            throw new NetlifyClientDeployKeysException($"{nameof(NetlifyClientDeployKeysException)} : Can't create deploy key to host hub : {response.StatusCode}");
        }

        public async Task<string> PostCreateAsync(string newHubName, string credidentials)
        {
            //Create - POST
            //https://api.netlify.com/api/v1/sites?access_token={token}
            //        {
            //	        "name" : "TESTingMore"
            //        }
            var model = new CreateHubDTO()
            {
                Name = newHubName
            };

            var response = await this.Client.PostAsync($"sites?access_token={credidentials}", base.CreateHttpContent<CreateHubDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = GetCreatedResponse<HubProjectDTO>(responseBody);

                return resultIdModel.id;
            }

            throw new NetlifyClientPostCreateException($"{nameof(NetlifyClientPostCreateException)} : Can't create post to host hub : {response.StatusCode}");
        }
    }
}