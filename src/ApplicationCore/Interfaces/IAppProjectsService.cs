using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppProjectsService<T>
    {
        Task<T> GetClientProject(string clientId);

        Task AddStoreTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository, IEnumerable<Widget> widgets);

        Task AddBlogTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository, IEnumerable<Widget> widgets);
    }
}