using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceResetPasswordException : Exception
    {
        public AccountServiceResetPasswordException()
        {
        }

        public AccountServiceResetPasswordException(string message) : base(message)
        {
        }

        public AccountServiceResetPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceResetPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}