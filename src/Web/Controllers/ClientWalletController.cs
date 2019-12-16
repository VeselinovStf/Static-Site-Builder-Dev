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
        private readonly IClientWalletModelFactory modelFactory;
        private readonly IAppLogger<ClientWalletController> logger;

        public ClientWalletController(
            IWalletService<WalletDTO> walletService,
            ITokenService tokenService,
            IClientWalletModelFactory modelFactory,
            IAppLogger<ClientWalletController> logger)
        {
            this.walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            this.tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
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

                return View(model);
            }
            catch (Exception ex)
            {

                this.logger.LogWarning($"{nameof(ClientWalletController)} : {nameof(Wallet)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Get Client Wallet. Contact support" });
            }           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Wallet([Bind("WalletId")]ClientWalletViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.tokenService.AddToken(model.WalletId);

                    this.logger.LogInformation($"{nameof(ClientWalletController)} : {nameof(Wallet)} : Success in adding token to user wallet");

                    return RedirectToAction("Wallet", "ClientWallet", new { clientWalletId = model.WalletId });
                }
                catch (Exception ex)
                {

                    this.logger.LogWarning($"{nameof(ClientWalletController)} : {nameof(Wallet)} : Exception - {ex.Message}");

                    return RedirectToAction("Error", "Home", new { message = "Can't add tokens to Wallet. Contact support" });
                }
            }

            return View(model);
        }
    }
}
