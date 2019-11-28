using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAPIHostClientService<T>
    {
        Task<string> CreateHubAsync(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken);
    }
}