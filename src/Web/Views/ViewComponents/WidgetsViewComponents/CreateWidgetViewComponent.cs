using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.ViewComponents.WidgetsViewComponents
{
    public class CreateWidgetViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {            
                return View("CreateWidget");
          
        }
    }
}
