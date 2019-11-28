using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.RepoHubConnectorService.Exceptions
{
    public class RepoHubKeyMakerCreateKeyException : Exception
    {
        public RepoHubKeyMakerCreateKeyException()
        {
        }

        public RepoHubKeyMakerCreateKeyException(string message) : base(message)
        {
        }

        public RepoHubKeyMakerCreateKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepoHubKeyMakerCreateKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}