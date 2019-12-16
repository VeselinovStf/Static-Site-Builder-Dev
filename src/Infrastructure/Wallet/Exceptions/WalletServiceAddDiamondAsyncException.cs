using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Wallet.Exceptions
{
    public class WalletServiceAddDiamondAsyncException : Exception
    {
        public WalletServiceAddDiamondAsyncException()
        {
        }

        public WalletServiceAddDiamondAsyncException(string message) : base(message)
        {
        }

        public WalletServiceAddDiamondAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WalletServiceAddDiamondAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
