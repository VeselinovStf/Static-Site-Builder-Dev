using ApplicationCore.Interfaces;
using Infrastructure.SiteTypes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;

namespace Web.Controllers
{
    [Authorize]
    public class SiteTypeController : Controller
    {
        private readonly ISiteTypesService<SiteTypeDTO> siteTypesService;
        private readonly ISiteTypeModelFactory modelFactory;
        private readonly IAppLogger<SiteTypeController> logger;

        public SiteTypeController(
            ISiteTypesService<SiteTypeDTO> siteTypesService,
            ISiteTypeModelFactory modelFactory,
            IAppLogger<SiteTypeController> logger)
        {
            this.siteTypesService = siteTypesService ?? throw new ArgumentNullException(nameof(siteTypesService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> SelectSiteType(string clientId)
        {
            try
            {
                var serviceCall = await this.siteTypesService.GetAllTypesAsync();

                this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(SelectSiteType)} : Sucess - Getting Site Types");

                var model = this.modelFactory.Create(serviceCall, clientId);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(SelectSiteType)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display site types. Contact support" });
            }
        }
    }
}