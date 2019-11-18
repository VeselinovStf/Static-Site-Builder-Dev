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

        //TODO: Take note about this
        public string AuthorName { get; set; }

        private List<Comment> _comments = new List<Comment>();

        public IEnumerable<Comment> Comments
        {
            get
            {
                return new List<Comment>(_comments);
            }
        }

        public void AddComment(string postId, string authorId, string authorName,
            DateTime pubDate, string content)
        {
            this._comments.Add(new Comment()
            {
                PostId = postId,
                AuthorId = authorId,
                AuthorName = authorName,
                PubDate = pubDate,
                Content = content
            });
        }
    }
}