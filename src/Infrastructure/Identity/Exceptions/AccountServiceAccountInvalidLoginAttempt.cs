using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceAccountInvalidLoginAttempt : Exception
    {
        public AccountServiceAccountInvalidLoginAttempt()
        {
        }

        public AccountServiceAccountInvalidLoginAttempt(string message) : base(message)
        {
        }

        public AccountServiceAccountInvalidLoginAttempt(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceAccountInvalidLoginAttempt(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
