using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteTypesService<T>
    {
        /// <summary>
        /// Get all build in site types
        /// </summary>
        /// <returns>List of build in site types</returns>
        Task<IEnumerable<T>> GetAllTypesAsync();

        /// <summary>
        /// Confirm if site type exist in data
        /// </summary>
        /// <param name="buildInType">Type passed as string by user actuin</param>
        /// <returns>Found or not ot exception</returns>
        Task<bool> ConfirmTypeAsync(string buildInType);

        /// <summary>
        /// Creates new client site
        /// </summary>
        Task CreateAsync(
            string name, string description, string clientId,
            string buildInType, string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository);
    }
}