using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ModelFatories.MessagesModelFactory.Abstraction;
using Web.ViewModels.Messages;

namespace Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IAppMessageService messageService;
        private readonly IMessagesModelFactory modelFactory;
        private readonly IAppLogger<MessagesController> logger;

        public MessagesController(
            IAppMessageService messageService,
            IMessagesModelFactory modelFactory,
            IAppLogger<MessagesController> logger)
        {
            this.messageService = messageService ?? throw new System.ArgumentNullException(nameof(messageService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public IActionResult Index(string clientId)
        {
            var model = new MailBoxViewModel();

            return View(model);
        }
    }
}