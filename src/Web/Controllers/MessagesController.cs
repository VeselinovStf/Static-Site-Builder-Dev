﻿using ApplicationCore.Interfaces;
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
        private readonly IMessagesModelFactory modelFactory;
        private readonly IAppLogger<MessagesController> logger;

        public MessagesController(
            IMailBoxService<MailBoxDTO> mailBoxService,
            IMessagesModelFactory modelFactory,
            IAppLogger<MessagesController> logger)
        {
            this.mailBoxService = mailBoxService ?? throw new System.ArgumentNullException(nameof(mailBoxService));

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
                await this.mailBoxService.SendClientNewMessage(model.ClientOwnerId, model.To, model.Subject, model.Text, model.IsDraft);

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
    }
}