using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.HostingHubConnectorService.Exceptions
{
    public class HostingHubConnectorPrepareForHubCreationException : Exception
    {
        public HostingHubConnectorPrepareForHubCreationException()
        {
        }

        public HostingHubConnectorPrepareForHubCreationException(string message) : base(message)
        {
        }

        public HostingHubConnectorPrepareForHubCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HostingHubConnectorPrepareForHubCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}