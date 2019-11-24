using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.ViewModels.Template;

namespace Web.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult SelectTemplate(string buildInType, string clientId)
        {
            //Call db to get all availible build in template
            var model = new SelectTemplateViewModel()
            {
                ClientId = clientId,
                BuildInSiteTypeName = buildInType,
                SiteTypeTemplate = new List<SiteTemplateViewModel>()
                {
                    new SiteTemplateViewModel()
                    {
                        Name = "Default",
                        Description = "Default Build In Template",
                        Image = "NO IMAGE"
                    }
                }
            };

            return View(model);
        }
    }
}