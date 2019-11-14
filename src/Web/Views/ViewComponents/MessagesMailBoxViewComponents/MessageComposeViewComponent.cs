using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels.Messages;

namespace Web.Views.ViewComponents.MessagesMailBoxViewComponents
{
    public class MessageComposeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(MessageViewModel model)
        {
            return View("MessageCompose", model);
        }
    }
}