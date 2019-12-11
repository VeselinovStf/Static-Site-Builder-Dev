using ApplicationCore.Interfaces;
using Infrastructure.SiteTypes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.AdminSiteTypesModelFactory.Abstraction;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;
using Web.ViewModels.AdminSiteType;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminSiteTypesController : Controller
    {
        private readonly ISiteTypesService<SiteTypeDTO> siteTypeService;
        private readonly IAdminSiteTypesModelFactory modelFactory;
        private readonly IAppLogger<AdminController> logger;

        public AdminSiteTypesController(
            ISiteTypesService<SiteTypeDTO> siteTypeService,
            IAdminSiteTypesModelFactory modelFactory,
            IAppLogger<AdminController> logger)
        {
            this.siteTypeService = siteTypeService ?? throw new ArgumentNullException(nameof(siteTypeService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> SiteTypes(string clientId)
        {
            //TODO: do i nead id
            try
            {
                var serviceCall = await this.siteTypeService.GetAllTypesAsync();

                this.logger.LogInformation($"{nameof(AdminSiteTypesController)} : {nameof(SiteTypes)} : Geting administrated site type templates done.");

                var model = this.modelFactory.Create(serviceCall, clientId);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminSiteTypesController)} : {nameof(SiteTypes)} : Can't get administrated site type templates posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

        //Display site type
        //Create siteType
        //CreateTemplate
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public IActionResult CreateSiteType()
        {
            //get all siteTypeEnum
            try
            {
                var buildInSiteTypesCall = this.siteTypeService.GetBuildInSiteTypes();

                this.logger.LogInformation($"{nameof(AdminSiteTypesController)} : {nameof(CreateSiteType)} : Geting administrated site type done.");

                var model = this.modelFactory.Create(buildInSiteTypesCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminSiteTypesController)} : {nameof(CreateSiteType)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Get site types. Contact support" });
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateSiteType([Bind("Name", "Description", "SiteType")]CreateSiteTypeTemplateViewModel model)
        {

            return View();
        }
    }
}
