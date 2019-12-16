using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.ViewComponentModels;
using Web.ViewModels.Widget;

namespace Web.Views.ViewComponents.WidgetsViewComponents
{
    public class AvailibleWidgetsViewComponent : ViewComponent
    {
        private readonly ApplicationCore.Interfaces.IAppUserManager<Infrastructure.Identity.ApplicationUser> appUserManager;

        public AvailibleWidgetsViewComponent(ApplicationCore.Interfaces.IAppUserManager<ApplicationUser> appUserManager)
        {
            this.appUserManager = appUserManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<WidgetViewModel> model)
        {
            var client = await this.appUserManager.FindByNameAsync(User.Identity.Name);

            var resultModel = new AddAvailibleWidgetViewComponetViewModel()
            {
                
                Widgets = model.Select(w => new WidgetViewModel()
                {
                    ClientId = client.Id,
                    Dependency = w.Dependency,
                    Id = w.Id,
                    Description = w.Description,
                    Functionality = w.Functionality,
                    IsFree = w.IsFree,
                    IsOn = w.IsOn,
                    Name = w.Name,
                    Price = w.Price,
                    SiteTypeSpecification = w.SiteTypeSpecification,
                    UsebleSiteType = w.UsebleSiteType,
                    Version = w.Version,
                    Votes = w.Votes
                })
            };

            if (ModelState.IsValid)
            {
                return View("AvailibleWidgets", resultModel);
            }

            return View();
        }
    }
}
