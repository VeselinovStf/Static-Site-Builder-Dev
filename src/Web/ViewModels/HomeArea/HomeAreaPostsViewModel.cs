using System;

namespace Web.ViewModels.HomeArea
{
    public class HomeAreaPostsViewModel
    {
        public string PostId { get; set; }
        public string Header { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public DateTime PubDate { get; set; }

        public string AuthorName { get; set; }

        public int CommentsCount { get; set; }
    }
}