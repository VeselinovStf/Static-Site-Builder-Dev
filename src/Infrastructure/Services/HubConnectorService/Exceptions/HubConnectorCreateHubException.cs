using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.HubConnectorService.Exceptions
{
    public class HubConnectorCreateHubException : Exception
    {
        public HubConnectorCreateHubException()
        {
        }

        public HubConnectorCreateHubException(string message) : base(message)
        {
        }

        public HubConnectorCreateHubException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HubConnectorCreateHubException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}