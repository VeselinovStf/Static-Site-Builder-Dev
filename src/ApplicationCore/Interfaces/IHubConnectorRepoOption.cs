using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHubConnectorRepoOption
    {
        /// <summary>
        /// Add variables to Settings -> CI/CD to Repository
        /// </summary>
        /// <param name="hubId">Repository project ID</param>
        /// <param name="hostingId">Hosting project id</param>
        /// <returns>Done or fails</returns>
        Task<bool> AddCiCDVariables(string hubId, string hostingId);
    }
}