using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceChangeTutorialException : Exception
    {
        public AccountServiceChangeTutorialException()
        {
        }

        public AccountServiceChangeTutorialException(string message) : base(message)
        {
        }

        public AccountServiceChangeTutorialException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceChangeTutorialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
