using ApplicationCore.Entities.Wallet;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppWidgetCalculatorService : IAppWidgetCalculatorService
    {
        private readonly IAsyncRepository<Wallet> walletRepository;
        private readonly IAsyncRepository<Widget> widgetRepository;

        public AppWidgetCalculatorService(
            IAsyncRepository<Wallet> walletRepository,
            IAsyncRepository<Widget> widgetRepository)
        {
            this.walletRepository = walletRepository ?? throw new ArgumentNullException(nameof(walletRepository));
            this.widgetRepository = widgetRepository ?? throw new ArgumentNullException(nameof(widgetRepository));
        }

        public async Task<bool> CheckTakeTokensAsync(string clientId, decimal price)
        {
            var walletSpecification = new GetWalletByClientIdSpecification(clientId);

            var wallet = this.walletRepository.GetSingleBySpec(walletSpecification);

            var clientWalletTokens = wallet.AvailibleCredit;
            var tokensSub = clientWalletTokens - price;

            if (tokensSub > -1)
            {
                return true;
            }

            return false;

        }

        public async  Task<bool> TakeTokensAsync(string clientId, string widgetId)
        {
            var walletSpecification = new GetWalletByClientIdSpecification(clientId);

            var wallet = this.walletRepository.GetSingleBySpec(walletSpecification);

            var widget = await this.widgetRepository.GetByIdAsync(widgetId);

            var clientWalletTokens = wallet.AvailibleCredit;
            var totalPrice = widget.Price;

            var tokensSub = clientWalletTokens - totalPrice;


            if (tokensSub > -1)
            {

                wallet.AvailibleCredit -= totalPrice;

                await this.walletRepository.UpdateAsync(wallet);

                return true;


            }
            else
            {
                return false;
            }
        }
    }
}
