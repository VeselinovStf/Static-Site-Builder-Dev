using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Wallet.Exceptions
{
    public class WalletServiceGetWalletAsyncException : Exception
    {
        public WalletServiceGetWalletAsyncException()
        {
        }

        public WalletServiceGetWalletAsyncException(string message) : base(message)
        {
        }

        public WalletServiceGetWalletAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WalletServiceGetWalletAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
