using System.Collections.Generic;

namespace Web.ViewModels.Messages
{
    public class MailBoxViewModel
    {
        public IEnumerable<MessageViewModel> Inbox { get; set; }
        public IEnumerable<MessageViewModel> Send { get; set; }
        public IEnumerable<MessageViewModel> Drafts { get; set; }
        public IEnumerable<MessageViewModel> Trash { get; set; }
    }
}