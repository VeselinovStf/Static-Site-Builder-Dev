using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppBlogTypeSiteService<T>
    {
        Task EditClientBlogProjectAsync(string clientId, string name, string description, string newProjectLocation, string templateLocation, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository);

        Task DeleteClientBlogProjectAsync(string clientId);
    }
}