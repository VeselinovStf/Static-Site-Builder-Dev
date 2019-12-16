using Infrastructure.Wallet.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.ClientWallet;

namespace Web.ModelFatories.ClientWalletModelFactory.Abstraction
{
    public interface IClientWalletModelFactory
    {
        ClientWalletViewModel Create(WalletDTO serviceCall);
    }
}
