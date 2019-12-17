using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class TutorialController : Controller
    {
		private readonly IAccountService<ApplicationUser> accountService;
		private readonly IAppLogger<TutorialController> logger;

		public TutorialController(
			IAccountService<ApplicationUser> accountService,
			IAppLogger<TutorialController> logger)
		{
			this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}
        public async Task<IActionResult> Tutorial(string clientId)
        {
			try
			{
				var clientTutorial = await this.accountService.GetClientIsInTutorial(clientId);
				//check if client is in tutorial 

				return this.Json(new { success = clientTutorial });
			}
			catch (Exception ex)
			{

				this.logger.LogWarning($"{nameof(TutorialController)} : {nameof(Tutorial)} : Problem in getting tutorial : {ex.Message}");

				return RedirectToAction("Error", "Home", new { message = "Sorry but we have problem with Tutorial System, please try later or contact support for more info." });

			}
		}
    }
}
