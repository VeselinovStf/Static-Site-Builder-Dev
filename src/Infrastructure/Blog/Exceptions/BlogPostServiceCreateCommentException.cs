using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceCreateCommentException : Exception
    {
        public BlogPostServiceCreateCommentException()
        {
        }

        public BlogPostServiceCreateCommentException(string message) : base(message)
        {
        }

        public BlogPostServiceCreateCommentException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceCreateCommentException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}