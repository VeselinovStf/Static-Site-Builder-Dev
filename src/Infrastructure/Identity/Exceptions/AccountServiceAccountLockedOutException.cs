using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceAccountLockedOutException : Exception
    {
        public AccountServiceAccountLockedOutException()
        {
        }

        public AccountServiceAccountLockedOutException(string message) : base(message)
        {
        }

        public AccountServiceAccountLockedOutException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceAccountLockedOutException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
