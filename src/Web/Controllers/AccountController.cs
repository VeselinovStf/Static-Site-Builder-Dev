using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Web.ViewModels.Account;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IAppLogger<AccountController> logger;
        private readonly IEmailSender emailSender;

        public AccountController(
            IAccountService<ApplicationUser> accountService,
            IAppLogger<AccountController> logger,
            IEmailSender emailSender)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var model = new RegisterViewModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName","Email","Password","ConfirmPassword")] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceCallResultUser = await this.accountService.RegisterAccountAsync(model.UserName, model.Email, model.Password);

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(Register)} : Sucess - Creating User");

                    await this.accountService.AddToRoleAsync(serviceCallResultUser, "Client");

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(Register)} : Sucess - Adding User Role");

                    var confirmationCode = await this.accountService.GenerateEmailConfirmationTokenAsync(serviceCallResultUser);

                    var callBackUrl = Url.Action(
                        "ConfirmEmail", "Account",                        
                       new { userId = serviceCallResultUser.Id, code = confirmationCode },
                        protocol: Request.Scheme
                        );

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(Register)} : Sending Confirmation Email to Created user");

                    await this.emailSender.SendEmailAsync(serviceCallResultUser.Email, "Sonic Site Builder - Confirm Your Email",
                        $"Wellcome to SSB, Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callBackUrl)}'>Click Here</a>.");

                    //await this.accountService.SignInAsync(serviceCallResultUser, isPersistent: false);

                    return RedirectToAction("SucceededRegistration", "Account");

                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(Register)} : Exception - {ex.Message}");

                    return View();
                }
            }

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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                var user = await this.accountService.FindByIdAsync(userId);

                if (await this.accountService.ConfirmEmailAsync(user,code))
                {
                    return View();
                }
                else
                {
                    throw new InvalidOperationException($"Error confirming email for user");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ConfirmEmail)} : Exception - {ex.Message}");

                return NotFound($"Unable to load user");
            }
          
        }

        [AllowAnonymous]
        [HttpGet]
        public  IActionResult SucceededRegistration()
        {
            return View();
        }
    }
}
