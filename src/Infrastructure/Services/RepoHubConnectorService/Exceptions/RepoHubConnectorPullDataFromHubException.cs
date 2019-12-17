using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Services.RepoHubConnectorService.Exceptions
{
    public class RepoHubConnectorPullDataFromHubException : Exception
    {
        public RepoHubConnectorPullDataFromHubException()
        {
        }

        public RepoHubConnectorPullDataFromHubException(string message) : base(message)
        {
        }

        public RepoHubConnectorPullDataFromHubException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepoHubConnectorPullDataFromHubException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
