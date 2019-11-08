using System;
using System.Runtime.Serialization;

namespace Infrastructure.Guard.Exceptions
{
    public class ObjectIsNullException : Exception
    {
        public ObjectIsNullException()
        {
        }

        public ObjectIsNullException(string message) : base(message)
        {
        }

        public ObjectIsNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ObjectIsNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}