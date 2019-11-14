using System.Collections.Generic;

namespace Web.ViewModels.Messages
{
    public class MailBoxViewModel
    {
        public string ClientId { get; set; }
        public IEnumerable<MessageViewModel> Inbox { get; set; }
        public IEnumerable<MessageViewModel> Sent { get; set; }
        public IEnumerable<MessageViewModel> Drafts { get; set; }
        public IEnumerable<MessageViewModel> Trash { get; set; }
    }
}