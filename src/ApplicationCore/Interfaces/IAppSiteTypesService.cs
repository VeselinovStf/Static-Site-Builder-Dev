using ApplicationCore.Entities.SiteType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppSiteTypesService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetSiteTypeAsync(string siteTypeId);
        Task AddWidgetAsync(string siteTypeId, string widgetId);
    }
}