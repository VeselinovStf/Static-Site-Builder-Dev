using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceEditPostException : Exception
    {
        public BlogPostServiceEditPostException()
        {
        }

        public BlogPostServiceEditPostException(string message) : base(message)
        {
        }

        public BlogPostServiceEditPostException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceEditPostException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}