using Infrastructure.AdminSiteTypes.DTOs;
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
        AdminClientSiteTypesViewModel Create(IEnumerable<AdminSiteTypeDTO> serviceCall);

        CreateSiteTypeTemplateViewModel Create(IList<string> buildInSiteTypes);

    }
}
