using ApplicationCore.Interfaces;
using Infrastructure.Services.APIClientService.Clients;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService
{
    public class GitLabAPIClientService : IAPIClientService<GitLabHubClient>
    {
        private readonly GitLabHubClient client;

        public GitLabAPIClientService(
            GitLabHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<bool> CreateHubAsync(string name, string accesTokken)
        {
            return await client.PostCreateAsync(name, accesTokken);
        }
    }
}