using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Home(string clientId)
        {
            return View();
        }
    }
}