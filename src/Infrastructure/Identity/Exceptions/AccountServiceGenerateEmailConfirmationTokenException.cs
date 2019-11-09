using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceGenerateEmailConfirmationTokenException : Exception
    {
        public AccountServiceGenerateEmailConfirmationTokenException()
        {
        }

        public AccountServiceGenerateEmailConfirmationTokenException(string message) : base(message)
        {
        }

        public AccountServiceGenerateEmailConfirmationTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceGenerateEmailConfirmationTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
