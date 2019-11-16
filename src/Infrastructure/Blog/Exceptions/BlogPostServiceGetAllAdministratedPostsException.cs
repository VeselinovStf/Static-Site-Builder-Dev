using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceGetAllAdministratedPostsException : Exception
    {
        public BlogPostServiceGetAllAdministratedPostsException()
        {
        }

        public BlogPostServiceGetAllAdministratedPostsException(string message) : base(message)
        {
        }

        public BlogPostServiceGetAllAdministratedPostsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceGetAllAdministratedPostsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}