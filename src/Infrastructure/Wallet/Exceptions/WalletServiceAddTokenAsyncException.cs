using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Wallet.Exceptions
{
    public class WalletServiceAddTokenAsyncException : Exception
    {
        public WalletServiceAddTokenAsyncException()
        {
        }

        public WalletServiceAddTokenAsyncException(string message) : base(message)
        {
        }

        public WalletServiceAddTokenAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WalletServiceAddTokenAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
