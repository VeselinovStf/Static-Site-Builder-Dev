using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Site;

namespace Web.Controllers
{
    [Authorize]
    public class ProductWidgetController : Controller
    {
        public ProductWidgetController()
        {

        }

        public async Task<IActionResult> Product(string widgetId, string clientId, string templateName, string siteTypeId)
        {
            var model = new SiteRenderingViewModel()
            {
                ClientId = clientId,
                SiteTypeId = siteTypeId,
                PresentationLink = "nope",
                TemplateName = templateName
            };

            return View(model);
        }
    }
}
