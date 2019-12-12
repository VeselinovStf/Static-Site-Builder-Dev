using Infrastructure.Widgets.DTOs;
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

                    Version = w.Version,
                    Votes = w.Votes
                }))
               
            };
        }
    }
}
