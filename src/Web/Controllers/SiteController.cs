using Microsoft.AspNetCore.Mvc;
using System;
using Web.ViewModels.Site;

namespace Web.Controllers
{
    public class SiteController : Controller
    {
        public SiteController()
        {
        }

        public IActionResult Site(string clientId, string siteTypeId)
        {
            try
            {
                //CALL TO HOSTING BUILD AND GET THE URL FOR PREVIEW
                var model = new SiteRenderingViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}