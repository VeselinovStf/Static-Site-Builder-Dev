using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Site;

namespace Web.Controllers
{
    public class SiteWidgetController : Controller
    {
        public SiteWidgetController()
        {

        }

        public async Task<IActionResult> Render(string widgetId, string clientId, string templateName, string siteTypeId)
        {
            try
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
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
