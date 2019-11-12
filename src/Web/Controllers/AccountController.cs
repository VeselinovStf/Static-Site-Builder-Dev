using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Identity.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

            var model = new AccountRegisterViewModel();

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserName", "Email", "Password", "ConfirmPassword")] AccountRegisterViewModel model)
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
                        $"Wellcome to SSB, Please confirm your account by <a href='{callBackUrl}'>Click Here</a>.");

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
            await this.accountService.SignOutAsync();

            var model = new AccountLoginViewModel();

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email", "Password", "RememberMe")]AccountLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.accountService.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(Login)} : Sucess - User logged in");

                    return RedirectToAction("Index", "Home");
                }
                catch (AccountServiceAccountEmailNotConfirmedException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(Login)} : Exception - {ex.Message}");

                    return RedirectToAction("SucceededRegistration", "Account");
                }
                catch (AccountServiceAccountInvalidLoginAttempt ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(Login)} : Exception - {ex.Message}");

                    return View(new AccountLoginViewModel()
                    {
                        IsInvalid = true,
                        ErrorMessage = "Invalid Login Attempt"
                    });
                }
                catch (AccountServiceAccountLockedOutException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(Login)} : Exception - {ex.Message}");

                    return RedirectToAction("Lockout", "Account");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(Login)} : Exception - {ex.Message}");

                    return View(new AccountLoginViewModel()
                    {
                        IsInvalid = true,
                        ErrorMessage = "Invalid Login Attempt"
                    });
                }
            }

            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await this.accountService.SignOutAsync();

                this.logger.LogInformation($"{nameof(AccountController)} : {nameof(Login)} : Sucess - User logged out");

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AccountController)} : {nameof(Login)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Loggin out problem. Contact support" });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Lockout()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            try
            {
                if (await this.accountService.ConfirmEmailAsync(userId, code))
                {
                    return View();
                }
                else
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ConfirmEmail)} : User Email Confirmation Problem");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ConfirmEmail)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't Confirm Email. Contact support" });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SucceededRegistration()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            var model = new AccountForgotPasswordViewModel()
            {
                IsSend = false
            };

            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword([Bind("Email", "IsSend")]AccountForgotPasswordViewModel model)
        {
            var returnModel = new AccountForgotPasswordViewModel()
            {
                IsSend = model.IsSend
            };

            if (ModelState.IsValid)
            {
                returnModel.IsSend = true;

                try
                {
                    var serviceCallResultUser = await this.accountService.FindByEmailAsync(model.Email);

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(ForgotPassword)} : User Found.");

                    var confirmationCode = await this.accountService.GeneratePasswordResetTokenAsync(serviceCallResultUser);

                    var callBackUrl = Url.Action(
                        "ChangePassword", "Account",
                       new { code = confirmationCode },
                        protocol: Request.Scheme
                        );

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(ForgotPassword)} : Sending Email to user");

                    await this.emailSender.SendEmailAsync(serviceCallResultUser.Email, "Sonic Site Builder - Change Password",
                        $"Hello, from SSB. This is your request for password change <a href='{callBackUrl}'>Click Here</a>.");

                    this.logger.LogInformation($"{nameof(AccountController)} : {nameof(ForgotPassword)} : Sucess - Sending Password Reset Link logged in");

                    return View(returnModel);
                }
                catch (AccountServiceFindByEmailException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ForgotPassword)} : Exception - {ex.Message}");
                }
                catch (AccountServiceGeneratePasswordConfirmationTokenException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ForgotPassword)} : Exception - {ex.Message}");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ForgotPassword)} : Exception - {ex.Message}");
                }

                return View(returnModel);
            }

            return View(returnModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ChangePassword(string code)
        {
            try
            {
                if (await this.accountService.ConfirmCangePasswordAsync(code))
                {
                    var model = new AccountPasswordResetTokenViewModel()
                    {
                        Token = code
                    };

                    return View(model);
                }
                else
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ChangePassword)} : Error changing password for user");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ChangePassword)} : Exception - {ex.Message} : With CODE = {code}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't Do Your Change Password Request. Contact support" });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword([Bind("UserName", "Password", "ConfirmPassword", "Token")] AccountPasswordResetTokenViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await this.accountService.ResetPasswordAsync(model.UserName, model.Password, model.ConfirmPassword, model.Token))
                    {
                        this.logger.LogInformation($"{nameof(AccountController)} : {nameof(ChangePassword)} : Sucess - User change password.");

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ChangePassword)} : Error changing password for user");
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountController)} : {nameof(ChangePassword)} : Exception - {ex.Message}");
                }

                return RedirectToAction("Error", "Home", new { message = "Can't Do Your Change Password Request. Contact support" });
            }

            return View();
        }
    }
}