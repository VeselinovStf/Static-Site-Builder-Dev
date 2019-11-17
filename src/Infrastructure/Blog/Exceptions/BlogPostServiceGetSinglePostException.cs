using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceGetSinglePostException : Exception
    {
        public BlogPostServiceGetSinglePostException()
        {
        }

        public BlogPostServiceGetSinglePostException(string message) : base(message)
        {
        }

        public BlogPostServiceGetSinglePostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceGetSinglePostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}