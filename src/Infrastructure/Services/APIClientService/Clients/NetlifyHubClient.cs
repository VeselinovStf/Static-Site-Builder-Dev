using System.Net.Http;

namespace Infrastructure.Services.APIClientService.Clients
{
    public partial class NetlifyHubClient : BaseHubClient
    {
        private HttpClient Client { get; }

        public NetlifyHubClient(
            HttpClient client)
        {
            this.Client = client;
        }
    }
}