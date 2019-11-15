using System;
using System.Runtime.Serialization;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceFindByUserNameException : Exception
    {
        public AccountServiceFindByUserNameException()
        {
        }

        public AccountServiceFindByUserNameException(string message) : base(message)
        {
        }

        public AccountServiceFindByUserNameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceFindByUserNameException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}