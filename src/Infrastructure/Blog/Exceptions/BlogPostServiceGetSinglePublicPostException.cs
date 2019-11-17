using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceGetSinglePublicPostException : Exception
    {
        public BlogPostServiceGetSinglePublicPostException()
        {
        }

        public BlogPostServiceGetSinglePublicPostException(string message) : base(message)
        {
        }

        public BlogPostServiceGetSinglePublicPostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceGetSinglePublicPostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}