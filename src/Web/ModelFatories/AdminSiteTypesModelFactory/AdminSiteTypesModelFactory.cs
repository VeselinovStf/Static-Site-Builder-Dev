using Infrastructure.AdminSiteTypes.DTOs;
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
    }
}
