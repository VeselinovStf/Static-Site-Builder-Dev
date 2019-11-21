using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class SiteTypeController : Controller
    {
        public SiteTypeController()
        {
        }

        public IActionResult SelectSiteType(string clientId)
        {
            return View();
        }
    }
}