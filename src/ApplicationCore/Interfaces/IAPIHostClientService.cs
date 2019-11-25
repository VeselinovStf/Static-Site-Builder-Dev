using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAPIHostClientService<T>
    {
        Task<string> CreateHubAsync(string name, string accesTokken);
    }
}