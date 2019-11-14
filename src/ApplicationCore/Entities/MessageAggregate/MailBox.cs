using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities.MessageAggregate
{
    public class MailBox : BaseEntity, IAggregateRoot
    {
        private readonly List<Message> _messages = new List<Message>();

        public ICollection<Message> Messages
        {
            get
            {
                return new List<Message>(_messages);
            }
        }

        public string ClientId { get; set; }

        public void AddItem(
            string from, string to, string subject,
            string text, DateTime sendDate,
            bool isDraft = false, bool isTrash = false, bool isNew = true)
        {
            _messages.Add(new Message()
            {
                From = from,
                To = to,
                Subject = subject,
                Text = text,
                SendDate = sendDate,
                IsDraft = isDraft,
                IsTrash = isTrash,
                IsNew = isNew,
            });
        }
    }
}