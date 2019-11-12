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
        private readonly IAppLogger<AccountController> logger;

        public AccountManageController(
            IAccountService<ApplicationUser> accountService,
            IAccountManageModelFactory modelFactory,
            IAppLogger<AccountController> logger)
        {
            this.accountService = accountService ?? throw new System.ArgumentNullException(nameof(accountService));
            this.modelFactory = modelFactory ?? throw new System.ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> ChangeAccountInformation(string clientId)
        {
            try
            {
                var serviceModel = await this.accountService.FindByIdAsync(clientId);

                var model = this.modelFactory.Create(serviceModel);

                this.logger.LogInformation($"{nameof(ClientSettingsController)} : {nameof(ChangeAccountInformation)} : Showing user Settings account information");

                return View(model);
            }
            catch (AccountServiceFindByIdException ex)
            {
                this.logger.LogWarning($"{nameof(ClientSettingsController)} : {nameof(ChangeAccountInformation)} : Exception - {ex.Message}");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(ClientSettingsController)} : {nameof(ChangeAccountInformation)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't get to the User Setting. Contact support" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUserInformation(AccountManageUserInformationViewModel model)
        {
            logger.LogInformation($"--------------NAME {nameof(model)}");
            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(AccountManagePasswordViewModel model)
        {
            logger.LogInformation($"--------------NAME {nameof(model)}");

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccount(AccountManageDeleteViewModel model)
        {
            logger.LogInformation($"--------------NAME {nameof(model)}");

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payments(AccountManageDeleteViewModel model)
        {
            logger.LogInformation($"--------------NAME {nameof(model)}");

            return RedirectToAction("ChangeAccountInformation", "AccountManage", new { clientId = model.UserId });
        }
    }
}