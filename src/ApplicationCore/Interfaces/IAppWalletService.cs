using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppWalletService<T>
    {
        Task<T> GetWalletBuyClientIdAsync(string clientId);
        Task<T> GetWalletAsync(string clientWalletId);
        Task AddTokenAsync(string walledId);
        Task AddDiamandAsync(string walledId);
    }
}
