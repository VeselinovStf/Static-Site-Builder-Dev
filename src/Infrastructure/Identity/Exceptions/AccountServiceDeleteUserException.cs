using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceDeleteUserException : Exception
    {
        public AccountServiceDeleteUserException()
        {
        }

        public AccountServiceDeleteUserException(string message) : base(message)
        {
        }

        public AccountServiceDeleteUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceDeleteUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}