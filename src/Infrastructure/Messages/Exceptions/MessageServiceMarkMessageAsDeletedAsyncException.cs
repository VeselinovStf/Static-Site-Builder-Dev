using System;
using System.Runtime.Serialization;

namespace Infrastructure.Messages.Exceptions
{
    public class MessageServiceMarkMessageAsDeletedAsyncException : Exception
    {
        public MessageServiceMarkMessageAsDeletedAsyncException()
        {
        }

        public MessageServiceMarkMessageAsDeletedAsyncException(string message) : base(message)
        {
        }

        public MessageServiceMarkMessageAsDeletedAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MessageServiceMarkMessageAsDeletedAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}