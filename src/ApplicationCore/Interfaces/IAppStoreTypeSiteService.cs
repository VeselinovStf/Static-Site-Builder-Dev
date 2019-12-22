using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppStoreTypeSiteService<T>
    {
        Task<T> GetTypeWithUsedWidgetsSite(string siteTypeId);
        Task AddRangeOfWidgetsAsync(string id,IEnumerable<Widget> widgets);
        Task EditClientStoreProjectAsync(string clientId, string name, string description, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository);

        Task DeleteClientStoreProjectAsync(string clientId);
    }
}