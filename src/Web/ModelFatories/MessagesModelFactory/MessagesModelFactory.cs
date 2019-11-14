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
                Inbox = inputModel.Inbox != null ? new List<MessageViewModel>(inputModel.Inbox.Select(m => new MessageViewModel()
                {
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
    }
}