using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceGetRoleException : Exception
    {
        public AccountServiceGetRoleException()
        {
        }

        public AccountServiceGetRoleException(string message) : base(message)
        {
        }

        public AccountServiceGetRoleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceGetRoleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
