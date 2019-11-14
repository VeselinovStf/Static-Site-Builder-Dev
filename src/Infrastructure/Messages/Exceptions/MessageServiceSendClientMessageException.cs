using System;
using System.Runtime.Serialization;

namespace Infrastructure.Messages.Exceptions
{
    public class MessageServiceSendClientMessageException : Exception
    {
        public MessageServiceSendClientMessageException()
        {
        }

        public MessageServiceSendClientMessageException(string message) : base(message)
        {
        }

        public MessageServiceSendClientMessageException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MessageServiceSendClientMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}