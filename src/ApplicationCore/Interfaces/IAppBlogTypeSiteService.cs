using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppBlogTypeSiteService<T>
    {
        Task EditClientBlogProjectAsync(string clientId, string name, string description, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository);

        Task DeleteClientBlogProjectAsync(string clientId);
    }
}