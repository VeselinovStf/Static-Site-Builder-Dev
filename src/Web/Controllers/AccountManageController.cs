using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Identity.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.AccountManageModelFactory.Abstraction;
using Web.ViewModels.AccountManage;

namespace Web.Controllers
{
    [Authorize]
    public class AccountManageController : Controller
    {
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IAccountManageModelFactory modelFactory;
        private readonly IEmailSender emailSender;
        private readonly IAppLogger<AccountManageController> logger;

        public AccountManageController(
            IAccountService<ApplicationUser> accountService,
            IAccountManageModelFactory modelFactory,
            IEmailSender emailSender,
            IAppLogger<AccountManageController> logger)
        {
            this.accountService = accountService ?? throw new System.ArgumentNullException(nameof(accountService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> ChangeAccountInformation(string clientId)
        {
            try
            {
                var serviceModel = await this.accountService.FindByIdAsync(clientId);

                var model = this.modelFactory.Create(serviceModel);

                this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(ChangeAccountInformation)} : Showing user Settings account information");

                return View(model);
            }
            catch (AccountServiceFindByIdException ex)
            {
                this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(ChangeAccountInformation)} : Exception - {ex.Message}");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(ChangeAccountInformation)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't get to the User Setting. Contact support" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserInformation(AccountManageUserInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceCallResultUser = await this.accountService.FindByIdAsync(model.UserId);

                    this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : User Found.");

                    //UserName
                    var userUpdate = await this.accountService.UpdateUserName(serviceCallResultUser, model.UserName);

                    if (!userUpdate)
                    {
                        this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(ChangeAccountInformation)} : Can't Change User Name");

                        return RedirectToAction("Error", "Home", new { message = "Can't Update User Name. Contact support" });
                    }

                    //Email
                    if (serviceCallResultUser.Email.Equals(model.Email))
                    {
                        this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : Changing User Information Done");

                        return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
                    }
                    else
                    {
                        var emailUpdateConfirmationCode = await this.accountService.GenerateEmailConfirmationTokenAsync(serviceCallResultUser);

                        var changeEmailCall = await this.accountService.ChangeEmailAsync(serviceCallResultUser, model.Email, emailUpdateConfirmationCode);

                        if (changeEmailCall)
                        {
                            var callBackUrl = Url.Action(
                                     "ConfirmUserInformation", "Account",
                                    new
                                    {
                                        userId = serviceCallResultUser.Id,
                                        code = emailUpdateConfirmationCode,
                                        EmailConfig = true
                                    },
                                             protocol: Request.Scheme
                                     );

                            this.logger.LogInformation($"{nameof(AccountController)} : {nameof(UpdateUserInformation)} : Sending Email to user");

                            await this.emailSender.SendEmailAsync(serviceCallResultUser.Email, "Sonic Site Builder - Change Password",
                                $"Hello, from SSB. This is your request for User informacion change <a href='{callBackUrl}'>Click Here</a>.");

                            this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : Sucess - Sending User Information Reset Link logged in");

                            await this.accountService.SignOutAsync();

                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            this.logger.LogWarning($"{nameof(AccountController)} : {nameof(UpdateUserInformation)} : User Information Confirmation Problem");

                            return RedirectToAction("Error", "Home", new { message = "Can't Update User Indormation. Contact support" });
                        }
                    }
                }
                catch (AccountServiceFindByIdException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : Exception - {ex.Message}");
                }
                catch (AccountServiceUpdateUserNameException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : Exception - {ex.Message}");
                }
                catch (AccountServiceGeneratePasswordConfirmationTokenException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : Exception - {ex.Message}");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdateUserInformation)} : Exception - {ex.Message}");
                }
            }

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(AccountManagePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceCallResultUser = await this.accountService.FindByIdAsync(model.UserId);

                    this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(UpdatePassword)} : User Found.");

                    var confirmationCode = await this.accountService.GeneratePasswordResetTokenAsync(serviceCallResultUser);

                    var passwordChangeCall = await this.accountService.ResetPasswordAsync(serviceCallResultUser.UserName, model.Password, model.ConfirmPassword, confirmationCode);

                    if (passwordChangeCall)
                    {
                        this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(UpdatePassword)} : Sending Email to user");

                        var callBackUrl = Url.Action(
                        "ConfirmPasswordChange", "Account",
                       new
                       {
                           userId = serviceCallResultUser.Id,
                           code = confirmationCode
                       },
                                protocol: Request.Scheme
                        );

                        await this.emailSender.SendEmailAsync(serviceCallResultUser.Email, "Sonic Site Builder - Change Password",
                            $"Hello, from SSB. This is your request for password change <a href='{callBackUrl}'>Click Here</a>.");

                        this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(UpdatePassword)} : Sucess - Sending Password Reset Link logged in");

                        await this.accountService.SignOutAsync();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        this.logger.LogWarning($"{nameof(AccountController)} : {nameof(UpdatePassword)} : User Password Change Problem");

                        return RedirectToAction("Error", "Home", new { message = "Can't Update Password. Contact support" });
                    }
                }
                catch (AccountServiceFindByIdException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdatePassword)} : Exception - {ex.Message}");
                }
                catch (AccountServiceGeneratePasswordConfirmationTokenException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdatePassword)} : Exception - {ex.Message}");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(UpdatePassword)} : Exception - {ex.Message}");
                }
            }

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(AccountManageDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceCallResultUser = await this.accountService.FindByIdAsync(model.UserId);

                    this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(DeleteAccount)} : User Found.");

                    var deleteCallResult = await this.accountService.DeleteUser(serviceCallResultUser);

                    if (deleteCallResult)
                    {
                        this.logger.LogInformation(
                            $"{nameof(AccountManageController)} : {nameof(DeleteAccount)} : USER -- {model.UserId} -- {serviceCallResultUser.Email} -- {serviceCallResultUser.UserName} -- WAS DELETED!!");
                    }
                    else
                    {
                        this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(DeleteAccount)} : Can't Delete User");

                        return RedirectToAction("Error", "Home", new { message = "Can't Delete Account. Contact support" });
                    }

                    await this.accountService.SignOutAsync();

                    return RedirectToAction("Index", "Home");
                }
                catch (AccountServiceFindByIdException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(DeleteAccount)} : Exception - {ex.Message}");
                }
                catch (AccountServiceDeleteUserException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(DeleteAccount)} : Exception - {ex.Message}");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(DeleteAccount)} : Exception - {ex.Message}");
                }
            }

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payments(AccountManageDeleteViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var serviceCallResultUser = await this.accountService.FindByIdAsync(model.UserId);

                    this.logger.LogInformation($"{nameof(AccountManageController)} : {nameof(Payments)} : User Found.");

                    var confirmationCode = await this.accountService.GetPaymentsAsync(serviceCallResultUser);
                }
                catch (AccountServiceFindByIdException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(Payments)} : Exception - {ex.Message}");
                }
                catch (AccountServiceGetPaymentsException ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(Payments)} : Exception - {ex.Message}");
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(AccountManageController)} : {nameof(Payments)} : Exception - {ex.Message}");
                }
            }

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }
    }
}