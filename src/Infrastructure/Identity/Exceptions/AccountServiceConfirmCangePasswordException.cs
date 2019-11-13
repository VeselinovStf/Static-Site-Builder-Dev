using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceConfirmCangePasswordException : Exception
    {
        public AccountServiceConfirmCangePasswordException()
        {
        }

        public AccountServiceConfirmCangePasswordException(string message) : base(message)
        {
        }

        public AccountServiceConfirmCangePasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceConfirmCangePasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}