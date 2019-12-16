using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IWalletService<T>
    {
        Task<T> GetWalletByClientIdAsync(string clientId);
        Task<T> GetWalletAsync(string clientWalletId);
    }
}
