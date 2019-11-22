using Microsoft.AspNetCore.Mvc;
using System;

namespace Web.Controllers
{
    public class UseSiteController : Controller
    {
        public UseSiteController()
        {
        }

        public IActionResult Use(string clientId, string siteTypeId)
        {
            try
            {
                return RedirectToAction("Site", "SiteType", new { clientId = clientId, siteTypeId = siteTypeId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}