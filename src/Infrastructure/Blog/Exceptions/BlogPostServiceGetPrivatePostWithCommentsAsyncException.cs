using System;
using System.Runtime.Serialization;

namespace Infrastructure.Blog.Exceptions
{
    public class BlogPostServiceGetPrivatePostWithCommentsAsyncException : Exception
    {
        public BlogPostServiceGetPrivatePostWithCommentsAsyncException()
        {
        }

        public BlogPostServiceGetPrivatePostWithCommentsAsyncException(string message) : base(message)
        {
        }

        public BlogPostServiceGetPrivatePostWithCommentsAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BlogPostServiceGetPrivatePostWithCommentsAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}