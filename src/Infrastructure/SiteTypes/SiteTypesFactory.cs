using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.SiteTypes
{
    public abstract class SiteTypesFactory
    {
        public abstract Task Create(string clientProjectId,
            string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository, IEnumerable<Widget> widgets);
    }
}