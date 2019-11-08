using System;
using System.Runtime.Serialization;

namespace Infrastructure.Guard.Exceptions
{
    public class StringNotEqualToAnotherStringException : Exception
    {
        public StringNotEqualToAnotherStringException()
        {
        }

        public StringNotEqualToAnotherStringException(string message) : base(message)
        {
        }

        public StringNotEqualToAnotherStringException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StringNotEqualToAnotherStringException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}