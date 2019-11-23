using Infrastructure.Services.APIClientService.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class GitLabHubClient
    {
        public async Task<bool> PostCreateAsync(string newHubName, string credidentials)
        {
            //POST
            //CREATE PROJECT
            // https://gitlab.com/api/v4/projects?access_token=xz5NLCcyjkovPguXWzGC
            //{
            //	"name" : "Test2"
            //}
            var model = new CreateHubDTO()
            {
                Name = newHubName
            };

            var response = await this.Client.PostAsync($"projects?access_token={credidentials}", this.CreateHttpContent<CreateHubDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}