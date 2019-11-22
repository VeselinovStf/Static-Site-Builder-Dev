using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    public class LaunchSiteController : Controller
    {
        private readonly ILaunchSiteService launchService;
        private readonly IAppLogger<LaunchSiteController> logger;

        public LaunchSiteController(
            ILaunchSiteService launchService,
            IAppLogger<LaunchSiteController> logger)
        {
            this.launchService = launchService ?? throw new ArgumentNullException(nameof(launchService));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Launch(string clientId, string siteTypeId)
        {
            try
            {
                await this.launchService.LaunchSite(clientId, siteTypeId);

                this.logger.LogInformation($"{nameof(LaunchSiteController)} : {nameof(Launch)} : Sucess - Client Site is Launched");

                return RedirectToAction("Site", "SiteType", new { clientId = clientId, siteTypeId = siteTypeId });
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(LaunchSiteController)} : {nameof(Launch)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't launch project. Contact support" });
            }
        }

        public async Task<IActionResult> UnLaunch(string clientId, string siteTypeId)
        {
            try
            {
                await this.launchService.UnLaunchSite(clientId, siteTypeId);

                this.logger.LogInformation($"{nameof(LaunchSiteController)} : {nameof(Launch)} : Sucess - Client Site is UN-Launched");

                return RedirectToAction("Site", "SiteType", new { clientId = clientId, siteTypeId = siteTypeId });
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(LaunchSiteController)} : {nameof(UnLaunch)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't un-launch project. Contact support" });
            }
        }
    }
}