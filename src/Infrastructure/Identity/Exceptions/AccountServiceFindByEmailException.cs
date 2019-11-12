using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceFindByEmailException : Exception
    {
        public AccountServiceFindByEmailException()
        {
        }

        public AccountServiceFindByEmailException(string message) : base(message)
        {
        }

        public AccountServiceFindByEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceFindByEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
