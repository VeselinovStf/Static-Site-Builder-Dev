using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Wallet.Exceptions
{
    public class WalletServiceGetWalletByClientIdAsyncException : Exception
    {
        public WalletServiceGetWalletByClientIdAsyncException()
        {
        }

        public WalletServiceGetWalletByClientIdAsyncException(string message) : base(message)
        {
        }

        public WalletServiceGetWalletByClientIdAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WalletServiceGetWalletByClientIdAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
