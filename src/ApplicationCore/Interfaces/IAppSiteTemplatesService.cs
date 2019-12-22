using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppSiteTemplatesService<T>
    {
        Task<T> GetTemplateWithElementsAsync(string templateName);
        Task<T> GetByTemplateNameAsync(string templateName);

        Task<IEnumerable<T>> GetAllTemplatesByTypeAsync(string buildInType);
        Task<T> CreateTemplateAsync(string siteTypeId, string templateName, string description, decimal price);
        


        //Task AddVariablesAsync(string buildInSiteType, string templateName, string siteId, string accessToken);
    }
}