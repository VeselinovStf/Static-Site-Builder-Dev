using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.HostingHubConnectorService.Exceptions
{
    public class HostingHubConnectorCreateHubException : Exception
    {
        public HostingHubConnectorCreateHubException()
        {
        }

        public HostingHubConnectorCreateHubException(string message) : base(message)
        {
        }

        public HostingHubConnectorCreateHubException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HostingHubConnectorCreateHubException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}