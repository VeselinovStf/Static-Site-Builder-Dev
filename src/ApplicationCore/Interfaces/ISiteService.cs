using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteService<T>
    {
        Task UpdateSiteWidgetsAsync(string clientId, string defaultStoreSiteTemplateName, string siteTypeId);
        Task<T> RenderSiteAsync(string clientId, string siteTypeId);
    }
}
