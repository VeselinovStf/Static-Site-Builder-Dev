using System;

namespace Infrastructure.Messages.DTOs
{
    public class MessageDTO
    {
        public string Id { get; set; }
        public string ClientOwnerId { get; set; }
        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsDraft { get; set; }

        public bool IsTrash { get; set; }

        public bool IsNew { get; set; }
    }
}