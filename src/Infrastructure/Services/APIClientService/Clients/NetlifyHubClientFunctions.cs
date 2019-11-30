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

        public async Task<string> PostCreateAsync(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken)
        {
            //  POST        https://api.netlify.com/api/v1/sites?access_token={token}

            //{
            //   "repo":
            //      {
            //          "provider":"gitlab",
            //          "id":15568698,
            //          "repo":"VeselinovStf/finaltestnetlify",
            //          "private":true,
            //          "branch":"master",
            //          "cmd":"jekyll build",
            //          "dir":"_site/",
            //          "deploy_key_id":"5ddeaa5d59b987ba6113587a"
            //       }
            // }

            var model = new DeploySiteDTO()
            {
                Name = netlifySiteName,
                Repo = new DeployRepoDTO()
                {
                    Provider = "gitlab",
                    Id = repositoryId,
                    Repo = "VeselinovStf/" + repositoryName,
                    Private = true,
                    Branch = "master",
                    CMD = "jekyll build",
                    Dir = "_site/",
                    DeployKeyId = deployKeyId
                }
            };

            var response = await this.Client.PostAsync($"sites?access_token={accesToken}", base.CreateHttpContent<DeploySiteDTO>(model));

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