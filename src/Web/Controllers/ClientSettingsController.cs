using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Identity.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.ClientSettings.Abstraction;

namespace Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientSettingsController : Controller
    {
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly IClientSettingsModelFactory modelFactory;
        private readonly IAppLogger<ClientSettingsController> logger;

        public ClientSettingsController(
            IAccountService<ApplicationUser> accountService,
            IClientSettingsModelFactory modelFactory,
            IAppLogger<ClientSettingsController> logger)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Index(string clientId)
        {
            try
            {
                var serviceModel = await this.accountService.FindByIdAsync(clientId);

                var model = this.modelFactory.Create(serviceModel);

                this.logger.LogInformation($"{nameof(ClientSettingsController)} : {nameof(Index)} : Showing user Settings");

                return View(model);
            }
            catch (AccountServiceFindByEmailException ex)
            {
                this.logger.LogWarning($"{nameof(ClientSettingsController)} : {nameof(Index)} : Exception - {ex.Message}");
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(ClientSettingsController)} : {nameof(Index)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't Do Your Change Password Request. Contact support" });
        }
    }
}