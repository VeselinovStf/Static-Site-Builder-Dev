using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceAddToReleException : Exception
    {
        public AccountServiceAddToReleException()
        {
        }

        public AccountServiceAddToReleException(string message) : base(message)
        {
        }

        public AccountServiceAddToReleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceAddToReleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
