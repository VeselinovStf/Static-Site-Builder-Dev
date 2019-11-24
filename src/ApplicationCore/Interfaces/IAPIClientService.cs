using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAPIClientService<T>
    {
        Task<string> CreateHubAsync(string name, string accesTokken);

        Task<bool> PushDataToHub(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents);
    }
}