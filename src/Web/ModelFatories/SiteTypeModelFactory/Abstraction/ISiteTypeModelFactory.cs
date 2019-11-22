using Infrastructure.SiteTypes.DTOs;
using System.Collections.Generic;
using Web.ViewModels.SiteType;

namespace Web.ModelFatories.SiteTypeModelFactory.Abstraction
{
    public interface ISiteTypeModelFactory
    {
        ClientSiteTypesViewModel Create(IEnumerable<SiteTypeDTO> serviceCall, string clientId);

        SiteTypeEditViewModel Create(SiteTypeEditorDTO serviceCall);
    }
}