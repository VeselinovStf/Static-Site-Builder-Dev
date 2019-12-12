using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.AdminWidgets.Abstraction;
using Web.ViewModels.AdminWidgets;

namespace Web.ModelFatories.AdminWidgets
{
    public class AdminWidgetsModelFactory : IAdminWidgetsModelFactory
    {
        public AdminWidgetsListViewModel Create(AdminClientWidgetListDTO inputModel)
        {
            return new AdminWidgetsListViewModel()
            {
               
                AvailibleWidgets = new List<AdminWidgetViewModel>(inputModel.ClientWidgets.Select(w => new AdminWidgetViewModel()
                {
                    Id = w.Id,
                    Name = w.Name,
                    Description = w.Description,
                    Dependency = w.Dependency.ToString(),
                    Functionality = w.Functionality,
                    IsFree = w.IsFree,
                    IsOn = w.IsOn,
                    Price = w.Price,
                    SiteTypeSpecification = w.SiteTypeSpecification.ToString(),
                    Implementation = w.Implementation,
                    Version = w.Version,
                    Votes = w.Votes
                }))
               
            };
        }

        public CreateWidgetViewModel Create(IList<string> buildInSiteTypeWidgets, IList<string> buildInSiteTypes)
        {
            return new CreateWidgetViewModel()
            {
                SiteTypes = new List<SelectListItem>(buildInSiteTypes.Select(s => new SelectListItem()
                {
                    Text = s,
                    Value = s
                })),
                UsebleWidgetTypes = new List<SelectListItem>(buildInSiteTypeWidgets.Select(w => new SelectListItem()
                {
                    Text = w,
                    Value = w
                }))
            };
        }
    }
}
