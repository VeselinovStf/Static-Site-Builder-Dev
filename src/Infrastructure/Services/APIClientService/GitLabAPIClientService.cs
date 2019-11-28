using ApplicationCore.Interfaces;
using Infrastructure.Services.APIClientService.Clients;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService
{
    public class GitLabAPIClientService :
        IAPIRepoClientService<GitLabHubClient>,
        IRepoUserKey
    {
        private readonly GitLabHubClient client;

        public GitLabAPIClientService(
            GitLabHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<bool> AddKey(string accesToken, string key, string title)
        {
            return await this.client.AddRepoKey(accesToken, key, title);
        }

        public async Task<bool> AddVariables(string hubId, string accesToken, string hostingId, string hostingAccesToken)
        {
            var idCall = await client.AddHubVariables(hubId, accesToken, hostingId, "NETLIFY_SITE_ID");

            if (idCall)
            {
                return await client.AddHubVariables(hubId, accesToken, hostingAccesToken, "NETLIFY_AUTH_TOKEN");
            }

            return false;
        }

        public async Task<string> CreateHubAsync(string name, string accesTokken)
        {
            return await client.PostCreateAsync(name, accesTokken);
        }

        public async Task<bool> PushDataToHub(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents)
        {
            return await client.PushToHubAsync(hubId, accesTokken, filePaths, fileContents);
        }
    }
}