using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Widget;

namespace Web.Views.ViewComponents.WidgetsViewComponents
{
    public class AvailibleWidgetsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<WidgetViewModel> model)
        {
            if (ModelState.IsValid)
            {
                return View("AvailibleWidgets", model);
            }

            return View();
        }
    }
}
