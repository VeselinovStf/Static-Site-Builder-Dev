using Infrastructure.SiteTypes.DTOs;
using System.Collections.Generic;
using System.Linq;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;
using Web.ViewModels.SiteType;

namespace Web.ModelFatories.SiteTypeModelFactory
{
    public class SiteTypeModelFactory : ISiteTypeModelFactory
    {
        public ClientSiteTypesViewModel Create(IEnumerable<SiteTypeDTO> serviceCall, string clientId)
        {
            return new ClientSiteTypesViewModel()
            {
                ClientId = clientId,
                SiteTypes = new List<SiteTypeViewModel>(serviceCall.Select(t => new SiteTypeViewModel()
                {
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.BuildInName
                }))
            };
        }
    }
}