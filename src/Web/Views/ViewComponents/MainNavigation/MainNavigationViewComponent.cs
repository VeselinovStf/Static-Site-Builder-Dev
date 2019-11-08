using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Views.ViewComponents.MainNavigation
{
    public class MainNavigationViewComponent : ViewComponent
    {
        //private readonly IHttpContextAccessor httpContext;
        //private readonly IAccountService<Client> accountService;


        //public MainNavigationViewComponent(
        //    IHttpContextAccessor httpContext,
        //    IAccountService<Client> accountService,
        //    ILogger<MainNavigationViewComponent> logger)
        //{
        //    this.httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        //    this.accountService = accountService;

        //}

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
            

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    var user = this.httpContext.HttpContext.User;
        //    if (this.accountService.IsSignedIn(user))
        //    {

        //        var client = await this.accountService.RetrieveUserAsync(user);
        //        var role = await this.accountService.GetRolesAsync(client);

        //        var clientViewModel = new ClientViewComponentViewModel()
        //        {
        //            ClientId = client.Id
        //        };

        //        if (role.Contains("Client"))
        //        {


        //            return View("ClientMenu", clientViewModel);
        //        }
        //        else
        //        {
        //            if (role.Contains("Administrator"))
        //            {
        //                return View("AdministratorMenu", clientViewModel);
        //            }
        //        }

        //    }
        //    else
        //    {
        //        return View("GuestMenu");
        //    }

        //    return View();

        //}
    }
}
