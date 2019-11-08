using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Guard.Exceptions
{
    public class IntValueIsNegativeException : Exception
    {
        public IntValueIsNegativeException()
        {
        }

        public IntValueIsNegativeException(string message) : base(message)
        {
        }

        public IntValueIsNegativeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IntValueIsNegativeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
