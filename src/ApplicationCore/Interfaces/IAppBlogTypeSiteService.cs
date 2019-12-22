using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppBlogTypeSiteService<T>
    {
        Task AddRangeOfWidgetsAsync(string id, IEnumerable<Widget> widgets);
        Task EditClientBlogProjectAsync(string clientId, string name, string description, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository);

        Task DeleteClientBlogProjectAsync(string clientId);

        Task<T> GetTypeWithUsedWidgetsSite(string siteTypeId);
    }
}