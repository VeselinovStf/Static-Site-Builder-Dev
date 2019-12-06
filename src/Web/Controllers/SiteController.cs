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

        public async Task<IActionResult> Site(string clientId, string siteTypeId)
        {
            try
            {
                var serviceModel = await this.siteRenderingService.RenderSiteAsync(clientId, siteTypeId);

                this.logger.LogInformation($"{nameof(SiteController)} : {nameof(Site)} : Sucess - Getting Client Site");
          
                var model = this.modelFactory.Create(serviceModel);

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