using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceConfirmEmailException : Exception
    {
        public AccountServiceConfirmEmailException()
        {
        }

        public AccountServiceConfirmEmailException(string message) : base(message)
        {
        }

        public AccountServiceConfirmEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceConfirmEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
