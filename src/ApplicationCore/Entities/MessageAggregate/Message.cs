using ApplicationCore.Entities.BaseEntities;
using System;

namespace ApplicationCore.Entities.MessageAggregate
{
    public class Message : BaseEntity
    {
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsDraft { get; set; }

        public bool IsTrash { get; set; }

        public bool IsNew { get; set; }

        public bool IsSent { get; set; }

        public string MailBoxId { get; private set; }

        public MailBox MailBox { get; set; }
    }
}