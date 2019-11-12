using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels.AccountManage;

namespace Web.Views.ViewComponents.AccountManageViewComponents
{
    public class ManageDeleteViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AccountManageDeleteViewModel model)
        {
            return View("ManageDelete", model);
        }
    }
}