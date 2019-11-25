using ApplicationCore.Interfaces;
using Infrastructure.Services.APIClientService.Clients;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.APIClientService
{
    public class NetlifyApiClientService : IAPIHostClientService<NetlifyHubClient>
    {
        private readonly NetlifyHubClient client;

        public NetlifyApiClientService(
            NetlifyHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> CreateHubAsync(string name, string accesTokken)
        {
            return await client.PostCreateAsync(name, accesTokken);
        }
    }
}