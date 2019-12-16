using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IDiamondService
    {
        Task AddDiamond(string walletId);
        Task ExchangeToken(string walletId);
    }
}
