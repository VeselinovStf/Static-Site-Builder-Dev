using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHostingHubConnector
    {
        Task<string> CreateHub(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken);
    }
}