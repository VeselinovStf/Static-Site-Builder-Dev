using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceCreateAccountException : Exception
    {
        public AccountServiceCreateAccountException()
        {
        }

        public AccountServiceCreateAccountException(string message) : base(message)
        {
        }

        public AccountServiceCreateAccountException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceCreateAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
