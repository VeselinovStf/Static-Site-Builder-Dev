using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Identity;
using Infrastructure.Messages.DTOs;
using Infrastructure.Messages.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public class MessagesService : IMailBoxService<MailBoxDTO>
    {
        private readonly IAppMailBoxService mailBox;
        private readonly IAccountService<ApplicationUser> accountService;

        public MessagesService(
            IAppMailBoxService mailBox,
             IAccountService<ApplicationUser> accountService)
        {
            this.mailBox = mailBox ?? throw new ArgumentNullException(nameof(mailBox));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<MailBoxDTO> GetClientMailBox(string clientId)
        {
            Validator.StringIsNullOrEmpty(
             clientId, $"{nameof(MessagesService)} : {nameof(GetClientMailBox)} : {nameof(clientId)} : is null/empty");

            var call = await this.mailBox.GetClientMailBox(clientId);

            return new MailBoxDTO()
            {
                ClientId = call.ClientId,
                Inbox = call.Messages != null ? new List<MessageDTO>(
                    call.Messages
                    .Where(m => m.IsNew && !m.IsDeleted && !m.IsTrash && !m.IsDraft && !m.IsSent)
                    .ToList()
                    .Select(nm => new MessageDTO()
                    {
                        Id = nm.Id,
                        ClientOwnerId = clientId,
                        From = nm.From,
                        IsDraft = nm.IsDraft,
                        IsNew = nm.IsNew,
                        IsTrash = nm.IsTrash,
                        SendDate = nm.SendDate,
                        Subject = nm.Subject,
                        Text = nm.Text,
                        To = nm.To
                    })) : new List<MessageDTO>(),
                Drafts = call.Messages != null ? new List<MessageDTO>(
                    call.Messages
                    .Where(m => !m.IsDeleted && !m.IsTrash && m.IsDraft)
                    .ToList()
                    .Select(nm => new MessageDTO()
                    {
                        Id = nm.Id,
                        ClientOwnerId = clientId,
                        From = nm.From,
                        IsDraft = nm.IsDraft,
                        IsNew = nm.IsNew,
                        IsTrash = nm.IsTrash,
                        SendDate = nm.SendDate,
                        Subject = nm.Subject,
                        Text = nm.Text,
                        To = nm.To
                    })) : new List<MessageDTO>(),
                Sent = call.Messages != null ? new List<MessageDTO>(
                    call.Messages
                    .Where(m => !m.IsDeleted && !m.IsTrash && !m.IsDraft && m.IsSent)
                    .ToList()
                    .Select(nm => new MessageDTO()
                    {
                        Id = nm.Id,
                        ClientOwnerId = clientId,
                        From = nm.From,
                        IsDraft = nm.IsDraft,
                        IsNew = nm.IsNew,
                        IsTrash = nm.IsTrash,
                        SendDate = nm.SendDate,
                        Subject = nm.Subject,
                        Text = nm.Text,
                        To = nm.To
                    })) : new List<MessageDTO>(),
                Trash = call.Messages != null ? new List<MessageDTO>(
                    call.Messages
                    .Where(m => !m.IsDeleted && m.IsTrash && !m.IsDraft)
                    .ToList()
                    .Select(nm => new MessageDTO()
                    {
                        Id = nm.Id,
                        ClientOwnerId = clientId,
                        From = nm.From,
                        IsDraft = nm.IsDraft,
                        IsNew = nm.IsNew,
                        IsTrash = nm.IsTrash,
                        SendDate = nm.SendDate,
                        Subject = nm.Subject,
                        Text = nm.Text,
                        To = nm.To
                    })) : new List<MessageDTO>(),
            };
        }

        public async Task SendClientNewMessage(string clientOwnerId, string to, string subject, string text)
        {
            Validator.StringIsNullOrEmpty(
                clientOwnerId, $"{nameof(MessagesService)} : {nameof(SendClientNewMessage)} : {nameof(clientOwnerId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               to, $"{nameof(MessagesService)} : {nameof(SendClientNewMessage)} : {nameof(to)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               subject, $"{nameof(MessagesService)} : {nameof(SendClientNewMessage)} : {nameof(subject)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               text, $"{nameof(MessagesService)} : {nameof(SendClientNewMessage)} : {nameof(text)} : is null/empty");

            try
            {
                var currentFromUser = await this.accountService.FindByIdAsync(clientOwnerId);

                var currentToUser = await this.accountService.FindByUserNameAsync(to);

                Validator.ObjectIsNull(
                   currentFromUser, $"{nameof(MessagesService)} : {nameof(SendClientNewMessage)} : {nameof(currentFromUser)} : Can't find user with this id");

                Validator.ObjectIsNull(
                  currentToUser, $"{nameof(MessagesService)} : {nameof(SendClientNewMessage)} : {nameof(currentToUser)} : Can't find user with this id");

                await this.mailBox.SendClientMessage(currentFromUser.Id, currentFromUser.UserName, true, false, false, true, DateTime.Now, subject, text, to);

                await this.mailBox.SendClientMessage(currentToUser.Id, currentFromUser.UserName, true, false, false, false, DateTime.Now, subject, text, to);
            }
            catch (Exception ex)
            {
                throw new MessageServiceSendClientMessageException($"{nameof(MessageServiceSendClientMessageException)} : Can't send message to user : {ex.Message}");
            }
        }
    }
}