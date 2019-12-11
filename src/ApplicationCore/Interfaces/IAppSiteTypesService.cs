using ApplicationCore.Entities.SiteType;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppSiteTypesService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IList<SiteTypesEnum> GetSiteTypes();
    }
}