using ApplicationCore.Entities.Wallet;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public class AppWalletService : IAppWalletService<Wallet>
    {
        private readonly IAsyncRepository<Wallet> walletRepository;

        public AppWalletService(
            IAsyncRepository<Wallet> walletRepository)
        {
            this.walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
        }

        public async Task AddDiamandAsync(string walledId)
        {
            var wallet = await this.walletRepository.GetByIdAsync(walledId);

            wallet.AvailibleDiamons += 1;

            await this.walletRepository.UpdateAsync(wallet);
        }

        public async Task AddTokenAsync(string walledId)
        {
            var wallet = await this.walletRepository.GetByIdAsync(walledId);

            wallet.AvailibleCredit += 1;

            await this.walletRepository.UpdateAsync(wallet);
        }

        public async Task<Wallet> GetWalletAsync(string clientWalletId)
        {
            return await this.walletRepository.GetByIdAsync(clientWalletId);
        }

        public async Task<Wallet> GetWalletBuyClientIdAsync(string clientId)
        {
            var specification = new GetWalletByClientIdSpecification(clientId);

            return  this.walletRepository.GetSingleBySpec(specification);
        }
    }
}
