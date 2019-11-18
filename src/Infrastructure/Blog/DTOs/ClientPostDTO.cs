﻿using System;

namespace Infrastructure.Blog.DTOs
{
    public class ClientPostDTO
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