using ApplicationCore.Interfaces;
using Infrastructure.Site.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.SiteModelFactory.Abstraction;
using Web.ViewModels.Site;

namespace Web.Controllers
{
    [Authorize]
    public class SiteController : Controller
    {
        private readonly ISiteService<SiteRenderingDTO> siteRenderingService;
        private readonly ISiteModelFactory modelFactory;
        private readonly IAppLogger<SiteController> logger;

        public SiteController(
            ISiteService<SiteRenderingDTO> siteRenderingService,
            ISiteModelFactory modelFactory,
            IAppLogger<SiteController> logger)
        {
            this.siteRenderingService = siteRenderingService ?? throw new ArgumentNullException(nameof(siteRenderingService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Use(string clientId, string siteTemplateName, string siteTypeId, string returnUrl = "Site")
        {
            try
            {
                await this.siteRenderingService.UpdateSiteWidgetsAsync(clientId, siteTemplateName, siteTypeId);

                this.logger.LogInformation($"{nameof(SiteController)} : {nameof(Use)} : Sucess - Updating Site Widgets");

                return RedirectToAction(returnUrl, "Site", new { clientId = clientId, siteTemplateName = siteTemplateName, siteTypeId = siteTypeId });
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteController)} : {nameof(Use)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display client site. Contact support" });
            }
        }

        public async Task<IActionResult> Site(string clientId,string siteTemplateName, string siteTypeId)
        {
            try
            {
               
                var serviceModel = await this.siteRenderingService.RenderSiteAsync(clientId, siteTemplateName,siteTypeId);

                this.logger.LogInformation($"{nameof(SiteController)} : {nameof(Site)} : Sucess - Getting Client Site");
          
                var model = this.modelFactory.Create(serviceModel,siteTypeId);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteController)} : {nameof(Site)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display client site. Contact support" });
            }
        }
    }
}