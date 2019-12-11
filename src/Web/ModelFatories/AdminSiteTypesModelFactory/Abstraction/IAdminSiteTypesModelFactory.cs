using Infrastructure.SiteTypes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.AdminSiteType;

namespace Web.ModelFatories.AdminSiteTypesModelFactory.Abstraction
{
    public interface IAdminSiteTypesModelFactory
    {
        AdminClientSiteTypesViewModel Create(IEnumerable<SiteTypeDTO> serviceCall, string clientId);

        CreateSiteTypeTemplateViewModel Create(IList<string> buildInSiteTypes);

    }
}
