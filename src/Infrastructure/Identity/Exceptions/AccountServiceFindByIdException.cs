using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceFindByIdException : Exception
    {
        public AccountServiceFindByIdException()
        {
        }

        public AccountServiceFindByIdException(string message) : base(message)
        {
        }

        public AccountServiceFindByIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceFindByIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
