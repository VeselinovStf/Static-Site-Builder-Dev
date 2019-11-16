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
    public class MailBoxService : IMailBoxService<MailBoxDTO>, IMessageService<MessageDTO>
    {
        private readonly IAppMailBoxService mailBox;
        private readonly IAccountService<ApplicationUser> accountService;

        public MailBoxService(
            IAppMailBoxService mailBox,
             IAccountService<ApplicationUser> accountService)
        {
            this.mailBox = mailBox ?? throw new ArgumentNullException(nameof(mailBox));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<MailBoxDTO> GetClientMailBox(string clientId)
        {
            Validator.StringIsNullOrEmpty(
             clientId, $"{nameof(MailBoxService)} : {nameof(GetClientMailBox)} : {nameof(clientId)} : is null/empty");

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
                    .Where(m => !m.IsDeleted && !m.IsTrash && m.IsDraft && !m.IsSent)
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

        public async Task MarkMessageAsDeletedAsync(string clientId, string messageId)
        {
            Validator.StringIsNullOrEmpty(
              clientId, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsDeletedAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                messageId, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsDeletedAsync)} : {nameof(messageId)} : is null/empty");

            try
            {
                var currentFromUser = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                   currentFromUser, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsDeletedAsync)} : {nameof(currentFromUser)} : Can't find user with this id");

                await this.mailBox.DeleteMessage(clientId, messageId);
            }
            catch (Exception ex)
            {
                throw new MessageServiceMarkMessageAsDeletedAsyncException($"{nameof(MessageServiceMarkMessageAsDeletedAsyncException)} : Can't delete message : {ex.Message}");
            }
        }

        public async Task<MessageDTO> MarkMessageAsReadedAsync(string clientOwnerId, string messageId)
        {
            Validator.StringIsNullOrEmpty(
               clientOwnerId, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsReadedAsync)} : {nameof(clientOwnerId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               messageId, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsReadedAsync)} : {nameof(messageId)} : is null/empty");

            try
            {
                var userMessage = await this.accountService.FindByIdAsync(clientOwnerId);

                Validator.ObjectIsNull(
                   userMessage, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsReadedAsync)} : {nameof(userMessage)} : Can't find user with this id");

                var resultCall = await this.mailBox.ReadMessage(clientOwnerId, messageId);

                Validator.ObjectIsNull(
                   resultCall, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsReadedAsync)} : {nameof(resultCall)} : Can't read message");

                var returnModel = new MessageDTO()
                {
                    Id = resultCall.Id,
                    ClientOwnerId = clientOwnerId,
                    From = resultCall.From,
                    IsDraft = resultCall.IsDraft,
                    IsNew = resultCall.IsNew,
                    IsTrash = resultCall.IsTrash,
                    SendDate = resultCall.SendDate,
                    Subject = resultCall.Subject,
                    Text = resultCall.Text,
                    To = resultCall.To
                };

                return returnModel;
            }
            catch (Exception ex)
            {
                throw new MessageServiceMarkMessageAsReadedException($"{nameof(MessageServiceMarkMessageAsReadedException)} : Can't set message to be readed : {ex.Message}");
            }
        }

        public async Task MarkMessageAsTrashedAsync(string clientId, string messageId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsTrashedAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                messageId, $"{nameof(MailBoxService)} : {nameof(MarkMessageAsTrashedAsync)} : {nameof(messageId)} : is null/empty");

            try
            {
                var currentFromUser = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                   currentFromUser, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(currentFromUser)} : Can't find user with this id");

                await this.mailBox.TrashMessage(clientId, messageId);
            }
            catch (Exception ex)
            {
                throw new MessageServiceMarkMessageTrashedException($"{nameof(MessageServiceMarkMessageTrashedException)} : Can't trash message : {ex.Message}");
            }
        }

        public async Task SendClientNewMessage(string clientOwnerId, string to, string subject, string text, bool isDraft)
        {
            Validator.StringIsNullOrEmpty(
                clientOwnerId, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(clientOwnerId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               to, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(to)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               subject, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(subject)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               text, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(text)} : is null/empty");

            try
            {
                var currentFromUser = await this.accountService.FindByIdAsync(clientOwnerId);

                var currentToUser = await this.accountService.FindByUserNameAsync(to);

                Validator.ObjectIsNull(
                   currentFromUser, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(currentFromUser)} : Can't find user with this id");

                Validator.ObjectIsNull(
                  currentToUser, $"{nameof(MailBoxService)} : {nameof(SendClientNewMessage)} : {nameof(currentToUser)} : Can't find user with this id");

                if (isDraft)
                {
                    await this.mailBox.SendClientMessage(currentFromUser.Id, currentFromUser.UserName, true, true, false, false, DateTime.Now, subject, text, to);
                }
                else
                {
                    await this.mailBox.SendClientMessage(currentFromUser.Id, currentFromUser.UserName, true, false, false, true, DateTime.Now, subject, text, to);

                    await this.mailBox.SendClientMessage(currentToUser.Id, currentFromUser.UserName, true, false, false, false, DateTime.Now, subject, text, to);
                }
            }
            catch (Exception ex)
            {
                throw new MessageServiceSendClientMessageException($"{nameof(MessageServiceSendClientMessageException)} : Can't send message to user : {ex.Message}");
            }
        }
    }
}