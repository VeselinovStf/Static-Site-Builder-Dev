using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels.Messages;

namespace Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        public MessagesController()
        {
        }

        public IActionResult Index(string clientId)
        {
            var model = new MailBoxViewModel();

            return View(model);
        }
    }
}