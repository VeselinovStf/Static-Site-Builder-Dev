using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.ViewModels.AccountManage;

namespace Web.Views.ViewComponents.AccountManageViewComponents
{
    public class ManagePaymentsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AccountManagePaymentViewModel model)
        {
            return View("ManagePayments", model);
        }
    }
}