using System;

namespace Web.ViewModels.Blog
{
    public class AdministratedCommentsViewModel
    {
        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime PubDate { get; set; }

        public string Content { get; set; }
    }
}