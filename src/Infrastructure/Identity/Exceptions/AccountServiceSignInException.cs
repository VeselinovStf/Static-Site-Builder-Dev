using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceSignInException : Exception
    {
        public AccountServiceSignInException()
        {
        }

        public AccountServiceSignInException(string message) : base(message)
        {
        }

        public AccountServiceSignInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceSignInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
