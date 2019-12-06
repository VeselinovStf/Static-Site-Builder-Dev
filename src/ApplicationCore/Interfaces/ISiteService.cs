using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteService<T>
    {
        Task<T> RenderSiteAsync(string clientId, string siteTypeId);
    }
}
