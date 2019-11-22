using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppStoreTypeSiteService<T>
    {
        Task EditClientStoreProjectAsync(string clientId, string name, string description, string newProjectLocation, string templateLocation, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository);

        Task DeleteClientStoreProjectAsync(string clientId);
    }
}