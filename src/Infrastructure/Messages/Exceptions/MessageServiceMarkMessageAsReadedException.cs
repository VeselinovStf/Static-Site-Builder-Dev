using System;
using System.Runtime.Serialization;

namespace Infrastructure.Messages.Exceptions
{
    public class MessageServiceMarkMessageAsReadedException : Exception
    {
        public MessageServiceMarkMessageAsReadedException()
        {
        }

        public MessageServiceMarkMessageAsReadedException(string message) : base(message)
        {
        }

        public MessageServiceMarkMessageAsReadedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MessageServiceMarkMessageAsReadedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}