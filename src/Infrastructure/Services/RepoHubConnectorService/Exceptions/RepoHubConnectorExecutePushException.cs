using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.RepoHubConnectorService.Exceptions
{
    public class RepoHubConnectorExecutePushException : Exception
    {
        public RepoHubConnectorExecutePushException()
        {
        }

        public RepoHubConnectorExecutePushException(string message) : base(message)
        {
        }

        public RepoHubConnectorExecutePushException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepoHubConnectorExecutePushException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}