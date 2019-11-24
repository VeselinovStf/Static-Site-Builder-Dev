using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.HubConnectorService.Exceptions
{
    public class HubConnectorExecutePushException : Exception
    {
        public HubConnectorExecutePushException()
        {
        }

        public HubConnectorExecutePushException(string message) : base(message)
        {
        }

        public HubConnectorExecutePushException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HubConnectorExecutePushException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}