using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceGetPaymentsException : Exception
    {
        public AccountServiceGetPaymentsException()
        {
        }

        public AccountServiceGetPaymentsException(string message) : base(message)
        {
        }

        public AccountServiceGetPaymentsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceGetPaymentsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}