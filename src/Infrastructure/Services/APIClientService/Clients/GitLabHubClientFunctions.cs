using ApplicationCore.Interfaces;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.APIClientService.Exceptions;
using Infrastructure.Services.FileTransferrer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class GitLabHubClient
    {
        private readonly List<string> ImageExtensions = new List<string> { ".img", ".jpg", ".png", ".otf", ".eot", ".ttf", ".woff", ".woff2" };
      

      

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

        public async Task<RepoPullTemplateDTO> PullFromHubAsync(string hubId, string repositoryName, string accesTokken)
        {
            //GET all projects https://gitlab.com/api/v4/users/veselinovStf/projects?access_token=zVXJZFCZxTkiBdEkG7sb
            //pars to model with name
            //test for template : name== repository name
            //if ok
            //get id of project
            //GET current template project https://gitlab.com/api/v4/projects/15770843?access_token=zVXJZFCZxTkiBdEkG7sb
           var defaultStoreTypeSiteFileRead = await FileTransporter.FilesToList(@"H:\HUB\Static_Store_Builder-SSB-\Dev_V03\src\Web\BuildInTemplates\" + repositoryName);

            var response = new RepoPullTemplateDTO()
            {
                Elements = new List<ConvertedFileElementDTO>(defaultStoreTypeSiteFileRead.Select(e => new ConvertedFileElementDTO()
                {
                    FileContent = e.FileContent,
                    FilePath = e.FilePath
                }))
            };

            return response;
            //var response = await this.Client.GetAsync($"projects?access_tok");

            //response.EnsureSuccessStatusCode();

            //if (response.IsSuccessStatusCode)
            //{
            //    string responseBody = await response.Content.ReadAsStringAsync();

            //    var resultModel = new RepoPullTemplateDTO();

            //    return resultModel;
            //}

            throw new GitHubClientGetCreateException($"{nameof(GitHubClientGetCreateException)} : Can't pull get from repo hub : Check File Reading");
        }

        public async Task<bool> AddRepoKey(string accesToken, string key, string title)
        {
            var model = new RepoUserKeyDTO()
            {
                Key = key,
                Title = title
            };

            var response = await this.Client.PostAsync($"user/keys?access_token={accesToken}", base.CreateHttpContent<RepoUserKeyDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new GitHubClientPostCreateException($"{nameof(GitHubClientPostCreateException)} : Can't create post to repo hub : {response.StatusCode} : {response.RequestMessage}");
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
    }
}