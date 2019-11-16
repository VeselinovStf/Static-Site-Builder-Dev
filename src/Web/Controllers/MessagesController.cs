using ApplicationCore.Interfaces;
using Infrastructure.Identity.Exceptions;
using Infrastructure.Messages.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.MessagesModelFactory.Abstraction;
using Web.ViewModels.Messages;

namespace Web.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IMailBoxService<MailBoxDTO> mailBoxService;
        private readonly IMessageService<MessageDTO> messageService;
        private readonly IMessagesModelFactory modelFactory;
        private readonly IAppLogger<MessagesController> logger;

        public MessagesController(
            IMailBoxService<MailBoxDTO> mailBoxService,
            IMessageService<MessageDTO> messageService,
            IMessagesModelFactory modelFactory,
            IAppLogger<MessagesController> logger)
        {
            this.mailBoxService = mailBoxService ?? throw new System.ArgumentNullException(nameof(mailBoxService));
            this.messageService = messageService ?? throw new ArgumentNullException(nameof(messageService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Index(string clientId)
        {
            try
            {
                var serviceCall = await this.mailBoxService.GetClientMailBox(clientId);

                this.logger.LogInformation($"{nameof(MessagesController)} : {nameof(Index)} : Success in geting user messages");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(Index)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Display User Messages. Contact support" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendNewMessage([Bind("ClientOwnerId", "To", "Subject", "Text", "IsDraft")]MessageViewModel model)
        {
            try
            {
                await this.messageService.SendClientNewMessage(model.ClientOwnerId, model.To, model.Subject, model.Text, model.IsDraft);

                this.logger.LogInformation($"{nameof(MessagesController)} : {nameof(SendNewMessage)} : Success in sending user messages");

                return RedirectToAction(nameof(Index), "Messages", new { clientId = model.ClientOwnerId });
            }
            catch (AccountServiceFindByIdException ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(SendNewMessage)} : Exception - {ex.Message}");
            }
            catch (AccountServiceFindByUserNameException ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(SendNewMessage)} : Exception - {ex.Message}");

                ViewData["Error"] = "Can't find user to send message to..";

                return ViewComponent("MessageCompose", model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(SendNewMessage)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't Send User Messages. Contact support" });
        }

        public async Task<IActionResult> TrashMessage(string clientId, string messageId)
        {
            try
            {
                await this.messageService.MarkMessageAsTrashedAsync(clientId, messageId);

                this.logger.LogInformation($"{nameof(MessagesController)} : {nameof(TrashMessage)} : Success in trashing user messages");

                return RedirectToAction(nameof(Index), "Messages", new { clientId = clientId });
            }
            catch (AccountServiceFindByIdException ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(TrashMessage)} : Exception - {ex.Message}");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(TrashMessage)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't Trash User Messages. Contact support" });
        }

        public async Task<IActionResult> DeleteMessage(string clientId, string messageId)
        {
            try
            {
                await this.messageService.MarkMessageAsDeletedAsync(clientId, messageId);

                this.logger.LogInformation($"{nameof(MessagesController)} : {nameof(DeleteMessage)} : Success in deleting user messages");

                return RedirectToAction(nameof(Index), "Messages", new { clientId = clientId });
            }
            catch (AccountServiceFindByIdException ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(DeleteMessage)} : Exception - {ex.Message}");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(MessagesController)} : {nameof(DeleteMessage)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't Delete User Messages. Contact support" });
        }
    }
}