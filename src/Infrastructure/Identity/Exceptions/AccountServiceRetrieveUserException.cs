using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceRetrieveUserException : Exception
    {
        public AccountServiceRetrieveUserException()
        {
        }

        public AccountServiceRetrieveUserException(string message) : base(message)
        {
        }

        public AccountServiceRetrieveUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceRetrieveUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}