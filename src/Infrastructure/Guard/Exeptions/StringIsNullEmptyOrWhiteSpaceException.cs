using System;
using System.Runtime.Serialization;

namespace Infrastructure.Guard.Exceptions
{
    public class StringIsNullEmptyOrWhiteSpaceException : Exception
    {
        public StringIsNullEmptyOrWhiteSpaceException()
        {
        }

        public StringIsNullEmptyOrWhiteSpaceException(string message) : base(message)
        {
        }

        public StringIsNullEmptyOrWhiteSpaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StringIsNullEmptyOrWhiteSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}