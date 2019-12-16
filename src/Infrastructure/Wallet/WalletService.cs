using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Wallet.DTOs;
using Infrastructure.Wallet.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Wallet
{
    public class WalletService : IWalletService<WalletDTO>, ITokenService, IDiamondService
    {
        private readonly IAppWalletService<ApplicationCore.Entities.Wallet.Wallet> appWalletService;

        public WalletService(
            IAppWalletService<ApplicationCore.Entities.Wallet.Wallet> appWalletService)
        {
            this.appWalletService = appWalletService ?? throw new ArgumentNullException(nameof(appWalletService));
        }

        public async Task AddDiamond(string walletId)
        {
           
                Validator.StringIsNullOrEmpty(
                    walletId, $"{nameof(WalletService)} : {nameof(AddDiamond)} : {nameof(walletId)} : is null/empty");

                try
                {


                    var walletCall = await this.appWalletService.GetWalletAsync(walletId);

                    Validator.ObjectIsNull(
                        walletCall, $"{nameof(WalletService)} : {nameof(AddToken)} : {nameof(walletCall)} : Can't find client wallet with this id");

                    //TODO: WALLET - REAL LIFE GET FROM CLIENT CARD $ And bye tokens
                    await this.appWalletService.AddDiamandAsync(walletId);

                }
                catch (Exception ex)
                {
                    throw new WalletServiceAddDiamondAsyncException($"{nameof(WalletServiceAddDiamondAsyncException)} : Exception : Can't add diamond to client walled : {ex.Message}");
                }
            
        }

        public async Task AddToken(string walledId)
        {
            Validator.StringIsNullOrEmpty(
                walledId, $"{nameof(WalletService)} : {nameof(AddToken)} : {nameof(walledId)} : is null/empty");

            try
            {

               
                var walletCall = await this.appWalletService.GetWalletAsync(walledId);

                Validator.ObjectIsNull(
                    walletCall, $"{nameof(WalletService)} : {nameof(AddToken)} : {nameof(walletCall)} : Can't find client wallet with this id");

                //TODO: WALLET - REAL LIFE GET FROM CLIENT CARD $ And bye tokens
                await this.appWalletService.AddTokenAsync(walledId);
                
            }
            catch (Exception ex)
            {
                throw new WalletServiceAddTokenAsyncException($"{nameof(WalletServiceAddTokenAsyncException)} : Exception : Can't add token to client walled : {ex.Message}");
            }
        }

        public Task ExchangeToken(string walletId)
        {
            throw new NotImplementedException();
        }

        public async Task<WalletDTO> GetWalletAsync(string clientWalletId)
        {
            Validator.StringIsNullOrEmpty(
                 clientWalletId, $"{nameof(WalletService)} : {nameof(GetWalletAsync)} : {nameof(clientWalletId)} : is null/empty");

            try
            {

                var walletCall = await this.appWalletService.GetWalletAsync(clientWalletId);

                Validator.ObjectIsNull(
                    walletCall, $"{nameof(WalletService)} : {nameof(GetWalletByClientIdAsync)} : {nameof(walletCall)} : Can't find client wallet with this id");

                var returnModel = new WalletDTO()
                {
                    ClientId = walletCall.ClientId,
                    WalledId = walletCall.Id,
                    AwailibleTokens = walletCall.AvailibleCredit
                };

                return returnModel;
            }
            catch (Exception ex)
            {
                throw new WalletServiceGetWalletAsyncException($"{nameof(WalletServiceGetWalletAsyncException)} : Exception : Can't get client walled : {ex.Message}");
            }
        }

        public async Task<WalletDTO> GetWalletByClientIdAsync(string clientId)
        {
          
            Validator.StringIsNullOrEmpty(
                 clientId, $"{nameof(WalletService)} : {nameof(GetWalletByClientIdAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
             
                var walletCall = await this.appWalletService.GetWalletBuyClientIdAsync(clientId);

                Validator.ObjectIsNull(
                    walletCall, $"{nameof(WalletService)} : {nameof(GetWalletByClientIdAsync)} : {nameof(walletCall)} : Can't find client wallet with this id");

                var returnModel = new WalletDTO()
                {
                     ClientId = clientId,
                     WalledId = walletCall.Id,
                      AwailibleTokens = walletCall.AvailibleCredit,
                      AwailibleDiamonds = walletCall.AvailibleDiamons
                };

                return returnModel;
            }
            catch (Exception ex)
            {
                throw new WalletServiceGetWalletByClientIdAsyncException($"{nameof(WalletServiceGetWalletByClientIdAsyncException)} : Exception : Can't get client walled : {ex.Message}");
            }
        }
    }
}
