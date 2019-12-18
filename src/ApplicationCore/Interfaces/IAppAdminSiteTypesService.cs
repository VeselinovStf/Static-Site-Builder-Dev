using ApplicationCore.Entities.SiteType;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppAdminSiteTypesService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        IList<SiteTypesEnum> GetSiteTypes();
        Task<T> CreateSiteTypeAsync(string name, string description, SiteTypesEnum type, decimal price);
    }
}
