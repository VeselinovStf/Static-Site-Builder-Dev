using ApplicationCore.Interfaces;
using Infrastructure.Wallet.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.ClientWalletModelFactory.Abstraction;
using Web.ViewModels.ClientWallet;

namespace Web.Controllers
{
    public class ClientWalletController : Controller
    {
        private readonly IWalletService<WalletDTO> walletService;
        private readonly ITokenService tokenService;
        private readonly IDiamondService diamondService;
        private readonly IClientWalletModelFactory modelFactory;
        private readonly IAppLogger<ClientWalletController> logger;

        public ClientWalletController(
            IWalletService<WalletDTO> walletService,
            ITokenService tokenService,
            IDiamondService diamondService,
            IClientWalletModelFactory modelFactory,
            IAppLogger<ClientWalletController> logger)
        {
            this.walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            this.diamondService = diamondService ?? throw new ArgumentNullException(nameof(diamondService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Wallet(string clientWalletId)
        {
            try
            {
                var serviceCall = await this.walletService.GetWalletAsync(clientWalletId);

                this.logger.LogInformation($"{nameof(ClientWalletController)} : {nameof(Wallet)} : Success in geting user wallet");


                var model = this.modelFactory.Create(serviceCall);
                model.IsTokenOrDiamond = false;
                return View(model);
            }
            catch (Exception ex)
            {

                this.logger.LogWarning($"{nameof(ClientWalletController)} : {nameof(Wallet)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Get Client Wallet. Contact support" });
            }           
        }

        [HttpGet]
        
        public async Task<IActionResult> AddToken(string walletId)
        {

            try
            {

                await this.tokenService.AddToken(walletId);

                this.logger.LogInformation($"{nameof(ClientWalletController)} : {nameof(AddToken)} : Success in adding token to user wallet");             

                return RedirectToAction("Wallet", "ClientWallet", new { clientWalletId = walletId });


            }
            catch (Exception ex)
            {

                this.logger.LogWarning($"{nameof(ClientWalletController)} : {nameof(Wallet)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't add tokens to Wallet. Contact support" });
            }

        }

        [HttpGet]
        
        public async Task<IActionResult> AddDiamond(string walletId)
        {

            try
            {


           
                await this.diamondService.AddDiamond(walletId);

                this.logger.LogInformation($"{nameof(ClientWalletController)} : {nameof(AddDiamond)} : Success in adding diamond to user wallet");


                return RedirectToAction("Wallet", "ClientWallet", new { clientWalletId = walletId });


            }
            catch (Exception ex)
            {

                this.logger.LogWarning($"{nameof(ClientWalletController)} : {nameof(AddDiamond)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't add tokens to Wallet. Contact support" });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExchangeToken(string walletId)
        {

            try
            {
                //await this.diamondService.ExchangeToken(walletId);

                //this.logger.LogInformation($"{nameof(ClientWalletController)} : {nameof(ExchangeToken)} : Success in adding diamond to user wallet");


                //return RedirectToAction("Wallet", "ClientWallet", new { clientWalletId = walletId });
                return RedirectToAction("Error", "Home", new { message = "Sorry byt this Function is under construction. We are working on in, don't warry!" });

            }
            catch (Exception ex)
            {

                this.logger.LogWarning($"{nameof(ClientWalletController)} : {nameof(ExchangeToken)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't add tokens to Wallet. Contact support" });
            }

        }

    }
}
