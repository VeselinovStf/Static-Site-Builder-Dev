using ApplicationCore.Interfaces;
using Infrastructure.Messages.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.MessagesModelFactory.Abstraction;
using Web.ViewModels.Messages;

namespace Web.Views.ViewComponents.MessagesMailBoxViewComponents
{
    public class MessageReadViewComponent : ViewComponent
    {
        private readonly IMessageService<MessageDTO> messageService;
        private readonly IMessagesModelFactory modelFactory;
        private readonly IAppLogger<MessageReadViewComponent> logger;

        public MessageReadViewComponent(
            IMessageService<MessageDTO> messageService,
            IMessagesModelFactory modelFactory,
            IAppLogger<MessageReadViewComponent> logger)
        {
            this.messageService = messageService ?? throw new System.ArgumentNullException(nameof(messageService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<IViewComponentResult> InvokeAsync(MessageViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.IsNew)
                {
                    try
                    {
                        var serviceCall = await this.messageService.MarkMessageAsReadedAsync(model.ClientOwnerId, model.Id);

                        this.logger.LogInformation($"{nameof(MessageReadViewComponent)} : {nameof(InvokeAsync)} : Sucess - Message Is marked as Readed");

                        var resultModel = this.modelFactory.Create(serviceCall);

                        return View("MessageRead", model);
                    }
                    catch (Exception ex)
                    {
                        this.logger.LogWarning($"{nameof(MessageReadViewComponent)} : {nameof(InvokeAsync)} : Exception - {ex.Message}");

                        return View();
                    }
                }
                else
                {
                    return View("MessageRead", model);
                }
            }

            return View();
        }
    }
}