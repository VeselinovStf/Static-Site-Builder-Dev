using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Guard.Exceptions
{
    public class StringIsNotNullEmptyOrWhiteSpaceException : Exception
    {
        public StringIsNotNullEmptyOrWhiteSpaceException()
        {
        }

        public StringIsNotNullEmptyOrWhiteSpaceException(string message) : base(message)
        {
        }

        public StringIsNotNullEmptyOrWhiteSpaceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected StringIsNotNullEmptyOrWhiteSpaceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
