using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public string Header { get; set; }

        public string Image { get; set; }

        public string Content { get; set; }

        public DateTime PubDate { get; set; }

        public string AuthorId { get; set; }

        private List<Comment> _comments = new List<Comment>();

        public IEnumerable<Comment> Comments
        {
            get
            {
                return _comments.AsReadOnly();
            }
        }

        public void AddComment(string authorId,
            DateTime pubDate, string content)
        {
            this._comments.Add(new Comment()
            {
                AuthorId = authorId,
                PubDate = pubDate,
                Content = content
            });
        }
    }
}