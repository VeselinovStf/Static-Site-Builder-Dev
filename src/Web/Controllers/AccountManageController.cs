using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class AccountManageController : Controller
    {
        public async Task<IActionResult> ChangeAccountInformation(string userId)
        {
            return View();
        }
    }
}