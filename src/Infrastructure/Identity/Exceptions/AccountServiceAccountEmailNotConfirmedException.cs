using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceAccountEmailNotConfirmedException : Exception
    {
        public AccountServiceAccountEmailNotConfirmedException()
        {
        }

        public AccountServiceAccountEmailNotConfirmedException(string message) : base(message)
        {
        }

        public AccountServiceAccountEmailNotConfirmedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceAccountEmailNotConfirmedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
