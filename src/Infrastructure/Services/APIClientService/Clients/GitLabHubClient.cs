using ApplicationCore.Interfaces;
using Infrastructure.Services.FileTransferrer.DTOs;
using System.Net.Http;

namespace Infrastructure.Services.APIClientService.Clients
{
    /// <summary>
    ///
    /// </summary>
    public partial class GitLabHubClient : BaseHubClient
    {
        private HttpClient Client { get; }

        private IFileTransferrer<ConvertedFileElement> FileTransporter { get; }

        public GitLabHubClient(
            HttpClient client,
            IFileTransferrer<ConvertedFileElement> fileTransporter)
        {
            this.Client = client;
            this.FileTransporter = fileTransporter;
        }
    }
}