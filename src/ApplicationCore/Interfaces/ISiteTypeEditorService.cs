using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteTypeEditorService<T>
    {
        /// <summary>
        /// Gets client created Site Type
        /// </summary>
        /// <param name="clientId">Pre confirmed client id</param>
        /// <param name="siteTypeId">Build site type id</param>
        /// <returns>User site type</returns>
        Task<T> GetClientEditableSiteTypeAsync(string clientId, string siteTypeId);

        Task EditSiteTypeAsync(string name, string description, string clientId, string id, string newProjectLocation, string templateLocation, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository);

        Task DeleteSiteTypeAsync(string clientId, string siteTypeId);
    }
}