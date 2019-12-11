using ApplicationCore.Interfaces;
using Infrastructure.SiteTypes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;
using Web.ViewModels.AdminSiteType;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminSiteTypesController : Controller
    {
        private readonly ISiteTypesService<SiteTypeDTO> siteTypeService;
        private readonly ISiteTypeModelFactory siteTypeModelFactory;
        private readonly IAppLogger<AdminController> logger;

        public AdminSiteTypesController(
            ISiteTypesService<SiteTypeDTO> siteTypeService,
            ISiteTypeModelFactory siteTypeModelFactory,
            IAppLogger<AdminController> logger)
        {
            this.siteTypeService = siteTypeService ?? throw new ArgumentNullException(nameof(siteTypeService));
            this.siteTypeModelFactory = siteTypeModelFactory ?? throw new ArgumentNullException(nameof(siteTypeModelFactory));
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

                var model = this.siteTypeModelFactory.Create(serviceCall, clientId);

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
                var model = new CreateTemplateViewModel();

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(TemplateController)} : {nameof(CreateSiteType)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Get site types. Contact support" });
            }

        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateSiteType([Bind("Name", "Description", "SiteType")]CreateTemplateViewModel model)
        {

            return View();
        }
    }
}
