using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels.AccountManage;

namespace Web.Views.ViewComponents.AccountManageViewComponents
{
    public class ManagePasswordViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AccountManagePasswordViewModel model)
        {
            return View("ManagePassword", model);
        }
    }
}