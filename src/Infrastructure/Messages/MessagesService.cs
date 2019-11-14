using ApplicationCore.Interfaces;
using Infrastructure.Messages.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Messages
{
    public class MessagesService : IMailBoxService<MailBoxDTO>
    {
        private readonly IAppMailBoxService mailBox;

        public MessagesService(
            IAppMailBoxService mailBox)
        {
            this.mailBox = mailBox ?? throw new ArgumentNullException(nameof(mailBox));
        }

        public async Task<MailBoxDTO> GetClientMailBox(string clientId)
        {
            var call = await this.mailBox.GetClientMailBox(clientId);

            return new MailBoxDTO()
            {
                Inbox = call.Messages != null ? new List<MessageDTO>(
                    call.Messages
                    .Where(m => m.IsNew && !m.IsDeleted && !m.IsTrash && !m.IsDraft)
                    .ToList()
                    .Select(nm => new MessageDTO()
                    {
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
                    .Where(m => !m.IsDeleted && !m.IsTrash && !m.IsDraft && m.From == clientId)
                    .ToList()
                    .Select(nm => new MessageDTO()
                    {
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
    }
}