using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppSiteTemplatesService<T>
    {
        Task<T> GetTemplateWithElementsAsync(string templateName);

        Task<IEnumerable<T>> GetAllTemplatesByTypeAsync(string buildInType);

        Task AddVariablesAsync(string buildInSiteType, string templateName, string siteId, string accessToken);
    }
}