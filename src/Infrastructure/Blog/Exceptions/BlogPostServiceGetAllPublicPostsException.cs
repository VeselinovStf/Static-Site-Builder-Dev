using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceGetAllPublicPostsException : Exception
    {
        public BlogPostServiceGetAllPublicPostsException()
        {
        }

        public BlogPostServiceGetAllPublicPostsException(string message) : base(message)
        {
        }

        public BlogPostServiceGetAllPublicPostsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceGetAllPublicPostsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}