using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.ViewModels.Messages;

namespace Web.Views.ViewComponents.MessagesMailBoxViewComponents
{
    public class MessageTrashViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IEnumerable<MessageViewModel> model)
        {
            return View("MessageTrash", model);
        }
    }
}