using Infrastructure.Messages.DTOs;
using System.Collections.Generic;
using System.Linq;
using Web.ModelFatories.MessagesModelFactory.Abstraction;
using Web.ViewModels.Messages;

namespace Web.ModelFatories.MessagesModelFactory
{
    public class MessagesModelFactory : IMessagesModelFactory
    {
        public MailBoxViewModel Create(MailBoxDTO inputModel)
        {
            return new MailBoxViewModel()
            {
                ClientId = inputModel.ClientId,
                Inbox = inputModel.Inbox != null ? new List<MessageViewModel>(inputModel.Inbox.Select(m => new MessageViewModel()
                {
                    Id = m.Id,
                    ClientOwnerId = m.ClientOwnerId,
                    From = m.From,
                    IsDraft = m.IsDraft,
                    IsNew = m.IsNew,
                    IsTrash = m.IsTrash,
                    SendDate = m.SendDate,
                    Subject = m.Subject,
                    Text = m.Text,
                    To = m.To
                })) : new List<MessageViewModel>(),
                Drafts = inputModel.Drafts != null ? new List<MessageViewModel>(inputModel.Drafts.Select(m => new MessageViewModel()
                {
                    Id = m.Id,
                    ClientOwnerId = m.ClientOwnerId,
                    From = m.From,
                    IsDraft = m.IsDraft,
                    IsNew = m.IsNew,
                    IsTrash = m.IsTrash,
                    SendDate = m.SendDate,
                    Subject = m.Subject,
                    Text = m.Text,
                    To = m.To
                })) : new List<MessageViewModel>(),
                Sent = inputModel.Sent != null ? new List<MessageViewModel>(inputModel.Sent.Select(m => new MessageViewModel()
                {
                    Id = m.Id,
                    ClientOwnerId = m.ClientOwnerId,
                    From = m.From,
                    IsDraft = m.IsDraft,
                    IsNew = m.IsNew,
                    IsTrash = m.IsTrash,
                    SendDate = m.SendDate,
                    Subject = m.Subject,
                    Text = m.Text,
                    To = m.To
                })) : new List<MessageViewModel>(),
                Trash = inputModel.Trash != null ? new List<MessageViewModel>(inputModel.Trash.Select(m => new MessageViewModel()
                {
                    Id = m.Id,
                    ClientOwnerId = m.ClientOwnerId,
                    From = m.From,
                    IsDraft = m.IsDraft,
                    IsNew = m.IsNew,
                    IsTrash = m.IsTrash,
                    SendDate = m.SendDate,
                    Subject = m.Subject,
                    Text = m.Text,
                    To = m.To
                })) : new List<MessageViewModel>(),
            };
        }

        public MessageViewModel Create(MessageDTO inputModel)
        {
            return new MessageViewModel()
            {
                Id = inputModel.Id,
                ClientOwnerId = inputModel.ClientOwnerId,
                From = inputModel.From,
                IsDraft = inputModel.IsDraft,
                IsNew = inputModel.IsNew,
                IsTrash = inputModel.IsTrash,
                SendDate = inputModel.SendDate,
                Subject = inputModel.Subject,
                Text = inputModel.Text,
                To = inputModel.To
            };
        }
    }
}