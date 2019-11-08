using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceIsSignInException : Exception
    {
        public AccountServiceIsSignInException()
        {
        }

        public AccountServiceIsSignInException(string message) : base(message)
        {
        }

        public AccountServiceIsSignInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceIsSignInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
