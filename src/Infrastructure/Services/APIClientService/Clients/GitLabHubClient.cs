using System.Net.Http;

namespace Infrastructure.Services.APIClientService.Clients
{
    /// <summary>
    ///
    /// </summary>
    public partial class GitLabHubClient : BaseHubClient
    {
        private HttpClient Client { get; }

        public GitLabHubClient(
            HttpClient client)
        {
            this.Client = client;
        }
    }
}