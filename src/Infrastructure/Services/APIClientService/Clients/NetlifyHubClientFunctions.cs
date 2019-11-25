using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.APIClientService.Exceptions;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class NetlifyHubClient
    {
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