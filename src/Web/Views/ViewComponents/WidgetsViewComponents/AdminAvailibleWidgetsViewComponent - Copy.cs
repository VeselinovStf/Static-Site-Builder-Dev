using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.AdminWidgets;
using Web.ViewModels.Widget;

namespace Web.Views.ViewComponents.WidgetsViewComponents
{
    public class AdminAvailibleWidgetsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<AdminWidgetViewModel> model)
        {
            if (ModelState.IsValid)
            {
                return View("AdminAvailibleWidgets", model);
            }

            return View();
        }
    }
}
