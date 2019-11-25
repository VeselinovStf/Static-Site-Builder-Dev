using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.RepoHubConnectorService.Exceptions
{
    public class RepoHubConnectorAddCiCDVariablesException : Exception
    {
        public RepoHubConnectorAddCiCDVariablesException()
        {
        }

        public RepoHubConnectorAddCiCDVariablesException(string message) : base(message)
        {
        }

        public RepoHubConnectorAddCiCDVariablesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RepoHubConnectorAddCiCDVariablesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}