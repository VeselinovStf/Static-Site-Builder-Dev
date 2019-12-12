using Infrastructure.AdminSiteTypes.DTOs;
using Infrastructure.AdminSiteTypeUsebleWidgets.DTOs;
using Infrastructure.SiteTypes.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.AdminSiteTypesModelFactory.Abstraction;
using Web.ViewModels.AdminSiteType;

namespace Web.ModelFatories.AdminSiteTypesModelFactory
{
    public class AdminSiteTypesModelFactory : IAdminSiteTypesModelFactory
    {
        public AdminClientSiteTypesViewModel Create(IEnumerable<AdminSiteTypeDTO> serviceCall)
        {
            return new AdminClientSiteTypesViewModel()
            {
                
                SiteTypes = new List<AdminSiteTypeViewModel>(serviceCall.Select(t => new AdminSiteTypeViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    Description = t.Description,
                    BuildInName = t.BuildInName
                }))
            };
        }

        public CreateSiteTypeTemplateViewModel Create(IList<string> buildInSiteTypes)
        {
            return new CreateSiteTypeTemplateViewModel()
            {
                SiteTypes = new List<SelectListItem>(buildInSiteTypes.Select(b => new SelectListItem()
                {
                    Value = b,
                    Text = b
                }))
            };
        }

        public AdminSiteTypeUsebleWidgetsViewModel Create(AdminSiteTypeUsebleWidgetsDTO serviceModel)
        {
            return new AdminSiteTypeUsebleWidgetsViewModel()
            {
                Id = serviceModel.Id,
                Description = serviceModel.Description,
                Name = serviceModel.Name,
                UsebleWidgets = new List<UsebleWidgetViewModel>(serviceModel.UsebleWidgets.Select(w => new UsebleWidgetViewModel()
                {
                    Description = w.Description,
                    Functionality = w.Functionality,
                    Name = w.Name,
                    Id = w.Id
                })),
                SiteTemplates = new List<AdminSiteTemplateViewModel>(serviceModel.SiteTemplates.Select(t => new AdminSiteTemplateViewModel()
                {
                    Name = t.Name,
                    Description = t.Description,
                    Id = t.Id
                }))
            };
        }
    }
}
