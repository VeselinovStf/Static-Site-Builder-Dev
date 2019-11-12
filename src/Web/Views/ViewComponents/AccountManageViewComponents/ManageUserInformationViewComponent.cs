using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels.AccountManage;

namespace Web.Views.ViewComponents.AccountManageViewComponents
{
    public class ManageUserInformationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AccountManageUserInformationViewModel model)
        {
            return View("ManageUserInformation", model);
        }
    }
}