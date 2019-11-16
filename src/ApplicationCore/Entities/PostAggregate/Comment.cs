﻿using System;

namespace ApplicationCore.Entities.PostAggregate
{
    public class Comment : BaseEntity
    {
        public string PostId { get; set; }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }

        public DateTime PubDate { get; set; }

        public string Content { get; set; }
    }
}