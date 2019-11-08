using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [AllowAnonymous]
        [HttpGet]        
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            //await this.accountService.SignOutAsync();

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}
