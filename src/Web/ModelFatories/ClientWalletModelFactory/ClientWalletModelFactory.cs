using Infrastructure.Wallet.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.ClientWalletModelFactory.Abstraction;
using Web.ViewModels.ClientWallet;

namespace Web.ModelFatories.ClientWalletModelFactory
{
    public class ClientWalletModelFactory : IClientWalletModelFactory
    {
        public ClientWalletViewModel Create(WalletDTO serviceCall)
        {
            return new ClientWalletViewModel()
            {
                AvailibleTokens = serviceCall.AwailibleTokens,
                WalletId = serviceCall.WalledId
            };
        }
    }
}
