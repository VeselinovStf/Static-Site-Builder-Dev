using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.Exceptions
{
    public class GitHubClientPostCreateException : Exception
    {
        public GitHubClientPostCreateException()
        {
        }

        public GitHubClientPostCreateException(string message) : base(message)
        {
        }

        public GitHubClientPostCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GitHubClientPostCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}