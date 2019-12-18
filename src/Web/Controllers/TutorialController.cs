using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class TutorialController : Controller
    {
        private readonly ITutorialService tutorialService;
        private readonly IAppLogger<TutorialController> logger;

        public TutorialController(
            ITutorialService tutorialService,

            IAppLogger<TutorialController> logger)
        {
            this.tutorialService = tutorialService ?? throw new ArgumentNullException(nameof(tutorialService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> ChangeTutorialStatus(string clientId)
        {
            try
            {
                
                 var tutorialChange = await this.tutorialService.ChangeTutorialStatusAsync(clientId);

                if (tutorialChange)
                {
                    this.logger.LogInformation($"{nameof(ClientController)} : {nameof(ChangeTutorialStatus)} : Changing client tutorial status done.");

                   
                }
                else
                {
                    this.logger.LogWarning($"{nameof(ClientController)} : {nameof(ChangeTutorialStatus)} : Problem in chnging client tutorial status done.");

                }

                return RedirectToAction("Index", "ClientSettings", new { clientId = clientId });
                
                
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(ClientController)} : {nameof(ChangeTutorialStatus)} : Can't get client posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't change Tutorial status, please try later or contact support for more info." });
            }
        }
    }
}
