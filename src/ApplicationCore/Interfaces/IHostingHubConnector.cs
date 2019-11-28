using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHostingHubConnector
    {
        Task<string> CreateHub(string name, string deployKeyId);
    }
}