using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAPIClientService<T>
    {
        Task<bool> CreateHubAsync(string name, string accesTokken);
    }
}