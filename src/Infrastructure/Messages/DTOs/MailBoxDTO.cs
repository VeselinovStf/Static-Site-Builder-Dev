using System.Collections.Generic;

namespace Infrastructure.Messages.DTOs
{
    public class MailBoxDTO
    {
        public string ClientId { get; set; }
        public IEnumerable<MessageDTO> Inbox { get; set; }
        public IEnumerable<MessageDTO> Sent { get; set; }
        public IEnumerable<MessageDTO> Drafts { get; set; }
        public IEnumerable<MessageDTO> Trash { get; set; }
    }
}