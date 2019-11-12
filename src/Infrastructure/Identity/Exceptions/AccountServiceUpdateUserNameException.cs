using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceUpdateUserNameException : Exception
    {
        public AccountServiceUpdateUserNameException()
        {
        }

        public AccountServiceUpdateUserNameException(string message) : base(message)
        {
        }

        public AccountServiceUpdateUserNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceUpdateUserNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}