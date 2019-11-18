using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceGetAllClientPostsWithCommentsException : Exception
    {
        public BlogPostServiceGetAllClientPostsWithCommentsException()
        {
        }

        public BlogPostServiceGetAllClientPostsWithCommentsException(string message) : base(message)
        {
        }

        public BlogPostServiceGetAllClientPostsWithCommentsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceGetAllClientPostsWithCommentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}