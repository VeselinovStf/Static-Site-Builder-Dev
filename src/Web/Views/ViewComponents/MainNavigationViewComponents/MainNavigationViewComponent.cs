using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.ViewComponentModels;

namespace Web.Views.ViewComponents.MainNavigationViewComponents
{
    public class MainNavigationViewComponent : ViewComponent
    {
        private readonly IHttpContextAccessor httpContext;
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IAppLogger<MainNavigationViewComponent> logger;

        public MainNavigationViewComponent(
            IHttpContextAccessor httpContext,
            IAccountService<ApplicationUser> accountService,
            IAppLogger<MainNavigationViewComponent> logger)
        {
            this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = this.httpContext.HttpContext.User;

            if (this.accountService.IsSignedIn(user))
            {
                var client = await this.accountService.RetrieveUserAsync(user);
                var role = await this.accountService.GetRolesAsync(client);

                var clientViewModel = new ClientViewComponentViewModel()
                {
                    ClientId = client.Id
                };

                if (role.Contains("Client"))
                {
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
    }
}