using System;
using System.Collections.Generic;

namespace Web.ViewModels.Blog
{
    public class ClientPostViewModel
    {
        public string PostId { get; set; }
        public string Header { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public DateTime PubDate { get; set; }

        public string AuthorName { get; set; }

        public IEnumerable<ClientCommentViewModel> Comments { get; set; }
    }
}