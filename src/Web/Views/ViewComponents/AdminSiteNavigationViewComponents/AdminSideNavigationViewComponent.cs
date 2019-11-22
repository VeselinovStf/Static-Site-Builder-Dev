using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Views.ViewComponents.AdminSiteNavigationViewComponents
{
    public class AdminSideNavigationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            return View("AdminSideNavigation");
        }
    }
}