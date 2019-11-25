using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.RepoHubConnectorService.Exceptions
{
    public class RepoHubConnectorCreateHubException : Exception
    {
        public RepoHubConnectorCreateHubException()
        {
        }

        public RepoHubConnectorCreateHubException(string message) : base(message)
        {
        }

        public RepoHubConnectorCreateHubException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepoHubConnectorCreateHubException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}