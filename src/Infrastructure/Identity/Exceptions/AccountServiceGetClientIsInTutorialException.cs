using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceGetClientIsInTutorialException : Exception
    {
        public AccountServiceGetClientIsInTutorialException()
        {
        }

        public AccountServiceGetClientIsInTutorialException(string message) : base(message)
        {
        }

        public AccountServiceGetClientIsInTutorialException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceGetClientIsInTutorialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
