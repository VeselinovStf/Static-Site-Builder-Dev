using System;
using System.Runtime.Serialization;

namespace Infrastructure.Messages.Exceptions
{
    public class MessageServiceMarkMessageTrashedException : Exception
    {
        public MessageServiceMarkMessageTrashedException()
        {
        }

        public MessageServiceMarkMessageTrashedException(string message) : base(message)
        {
        }

        public MessageServiceMarkMessageTrashedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MessageServiceMarkMessageTrashedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}