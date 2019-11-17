using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceCreatePostException : Exception
    {
        public BlogPostServiceCreatePostException()
        {
        }

        public BlogPostServiceCreatePostException(string message) : base(message)
        {
        }

        public BlogPostServiceCreatePostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceCreatePostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}