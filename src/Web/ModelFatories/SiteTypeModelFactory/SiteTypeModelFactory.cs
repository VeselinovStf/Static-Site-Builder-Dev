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
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.BuildInName,
                     Price = t.Price,
                      SiteTypeWidgets = new List<SiteTypeWidgetViewModel>(t.SiteTypeWidget.Select(s => new SiteTypeWidgetViewModel()
                      {
                           Price = s.Price,
                            WidgetId = s.WidgetId,
                             WidgetName = s.WidgetName,
                             IsAvailible = s.IsAvailible
                      }))
                }))
            };
        }

        public SiteTypeEditViewModel Create(SiteTypeEditorDTO serviceCall)
        {
            return new SiteTypeEditViewModel()
            {
                CardApiKey = serviceCall.CardApiKey,
                CardServiceGate = serviceCall.CardServiceGate,
                Description = serviceCall.Description,
                HostingServiceGate = serviceCall.HostingServiceGate,
                Name = serviceCall.Name,
                TemplateName = serviceCall.TemplateName,
                Repository = serviceCall.Repository,

                ClientId = serviceCall.ClientId,
                Id = serviceCall.Id,
                IsLaunched = serviceCall.IsLaunched
            };
        }
    }
}