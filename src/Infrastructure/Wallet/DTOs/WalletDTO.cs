using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Wallet.DTOs
{
   public class WalletDTO
    {
        public string ClientId { get; set; }

        public string WalledId { get; set; }

        public decimal AwailibleTokens { get; set; }
    }
}
