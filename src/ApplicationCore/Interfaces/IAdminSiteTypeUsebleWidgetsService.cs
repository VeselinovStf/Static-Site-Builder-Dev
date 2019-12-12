using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdminSiteTypeUsebleWidgetsService<T>
    {
        Task<T> GetSiteTypeAsync(string siteTypeId);
    }
}
