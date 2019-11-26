using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.APIClientService.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class GitLabHubClient
    {
        private readonly List<string> ImageExtensions = new List<string> { ".img", ".jpg", ".png" };

        public async Task<string> PostCreateAsync(string newHubName, string credidentials)
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

            var response = await this.Client.PostAsync($"projects?access_token={credidentials}", base.CreateHttpContent<CreateHubDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = GetCreatedResponse<HubProjectDTO>(responseBody);

                return resultIdModel.id;
            }

            throw new GitHubClientPostCreateException($"{nameof(GitHubClientPostCreateException)} : Can't create post to repo hub : {response.StatusCode}");
        }

        public async Task<bool> PushToHubAsync(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents)
        {
            var branch = "master";
            var commitMessage = "Initial";
            var actions = "create";

            var pushModel = new PushCreateDTO()
            {
                Branch = branch,
                CommitMessage = commitMessage,
                Actions = new List<HubFileDTO>(filePaths.Zip(fileContents, (fp, fc) => new HubFileDTO()
                {
                    Action = actions,
                    FilePath = fp,
                    Content = fc,
                    Encoding = this.ImageExtensions.Any(e => fp.Contains(e)) ? "base64" : "text"
                }))
            };

            var response = await this.Client.PostAsync($"projects/{hubId}/repository/commits?access_token={accesTokken}", base.CreateHttpContent<PushCreateDTO>(pushModel));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> AddHubVariables(string hubId, string repoAccesToken, string value, string key)
        {
            //POST /projects/:id/variables
            //key	string
            //value	string
            //$NETLIFY_SITE_ID --auth $NETLIFY_AUTH_TOKEN

            //var idVariablesModel = new RepoHubVariablesDTO()
            //{
            //    Key = "NETLIFY_SITE_ID",
            //    VariableType = "env_var",
            //    Value = hostingId
            //};

            //var authTokenVariablesModel = new RepoHubVariablesDTO()
            //{
            //    Key = "NETLIFY_AUTH_TOKEN",
            //    VariableType = "env_var",
            //    Value = hostingAccesToken
            //};

            var pushModel = new RepoHubVariablesDTO()
            {
                Key = key,
                VariableType = "env_var",
                Value = value,
                Environment_scope = "*",
                Masked = false,
                Protected = false
            };

            var response = await this.Client.PostAsync($"projects/{hubId}/variables?access_token={repoAccesToken}", base.CreateHttpContent<RepoHubVariablesDTO>(pushModel));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}