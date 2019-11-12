using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Identity.Exceptions
{
    public class AccountServiceGeneratePasswordConfirmationTokenException : Exception
    {
        public AccountServiceGeneratePasswordConfirmationTokenException()
        {
        }

        public AccountServiceGeneratePasswordConfirmationTokenException(string message) : base(message)
        {
        }

        public AccountServiceGeneratePasswordConfirmationTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountServiceGeneratePasswordConfirmationTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
