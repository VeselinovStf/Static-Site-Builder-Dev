using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdminSiteTypeService<T>
    {
        Task<IEnumerable<T>> GetAllTypesAsync();

        IList<string> GetBuildInSiteTypes();

        Task<T> AddSiteTypeAsync(string name, string description, string siteType);
    }
}
