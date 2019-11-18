using System;

namespace Infrastructure.Blog.DTOs
{
    public class ClientCommentsDTO
    {
        public string AuthorId { get; set; }

        public string AuthorName { get; set; }

        public DateTime PubDate { get; set; }

        public string Content { get; set; }
    }
}