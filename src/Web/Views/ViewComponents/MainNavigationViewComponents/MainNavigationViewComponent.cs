using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Wallet.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.ViewModels.ViewComponentModels;

namespace Web.Views.ViewComponents.MainNavigationViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IWalletService<WalletDTO> walletService;
        private readonly IAppLogger<MainNavigationViewComponent> logger;

        public MainNavigationViewComponent(
            IHttpContextAccessor httpContext,
            IAccountService<ApplicationUser> accountService,
            IWalletService<WalletDTO> walletService,
            IAppLogger<MainNavigationViewComponent> logger)
        {
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.walletService = walletService ?? throw new ArgumentNullException(nameof(walletService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            try
            {
                var user = this.httpContext.HttpContext.User;

                if (this.accountService.IsSignedIn(user))
                {
                    var client = await this.accountService.RetrieveUserAsync(user);
                    var role = await this.accountService.GetRolesAsync(client);

                    var clientViewModel = new MainNavigationComponentViewModel()
                    {
                        ClientId = client.Id,
                        
                    };

                    if (role.Contains("Client"))
                    {

                        var wallet = await this.walletService.GetWalletByClientIdAsync(client.Id);

                        clientViewModel.WalletAvailibleTokens = wallet.AwailibleTokens;
                        clientViewModel.WalledId = wallet.WalledId;

                        return View("ClientMenu", clientViewModel);
                    }
                    else
                    {
                        if (role.Contains("Administrator"))
                        {
                            return View("AdministratorMenu", clientViewModel);
                        }
                    }
                }
                else
                {
                    return View("GuestMenu");
                }

                return View();
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(MainNavigationViewComponent)} : {nameof(InvokeAsync)} : Exception : {ex.Message}");

                var model = new ErrorViewModel()
                {
                    Message = "Some error accure.. Please contact site support for help."
                };

                return View("DefaultError", model);
            }
        }
    }
}