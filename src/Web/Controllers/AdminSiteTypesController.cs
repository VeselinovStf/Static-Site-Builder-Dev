using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypes.DTOs;
using Infrastructure.AdminSiteTypeUsebleWidgets.DTOs;
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
        private readonly IAdminSiteTypeService<AdminSiteTypeDTO> siteTypeService;
        private readonly IAdminSiteTypeUsebleWidgetsService<AdminSiteTypeUsebleWidgetsDTO> siteTypeUsebleWidgetsService;
        private readonly IAdminSiteTypesModelFactory modelFactory;
        private readonly IAppLogger<AdminController> logger;

        public AdminSiteTypesController(
            IAdminSiteTypeService<AdminSiteTypeDTO> siteTypeService,
            IAdminSiteTypeUsebleWidgetsService<AdminSiteTypeUsebleWidgetsDTO> siteTypeUsebleWidgetsService,
            IAdminSiteTypesModelFactory modelFactory,
            IAppLogger<AdminController> logger)
        {
            this.siteTypeService = siteTypeService ?? throw new ArgumentNullException(nameof(siteTypeService));
            this.siteTypeUsebleWidgetsService = siteTypeUsebleWidgetsService ?? throw new ArgumentNullException(nameof(siteTypeUsebleWidgetsService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> SiteTypes()
        {
            //TODO: do i nead id
            try
            {
                var serviceCall = await this.siteTypeService.GetAllTypesAsync();

                this.logger.LogInformation($"{nameof(AdminSiteTypesController)} : {nameof(SiteTypes)} : Geting administrated site type templates done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminSiteTypesController)} : {nameof(SiteTypes)} : Can't get administrated site type templates posts : {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Sorry but we have problem with Blog System, please try later or contact support for more info." });
            }
        }

      
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

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSiteType([Bind("Name", "Description", "SiteType", "Price")]CreateSiteTypeTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var resultModel = await this.siteTypeService.AddSiteTypeAsync(model.Name, model.Description, model.SiteType, model.Price);

                    this.logger.LogInformation($"{nameof(AdminSiteTypesController)} : {nameof(CreateSiteType)} : Creating administrated site type done.");

                    return RedirectToAction("SiteTypes", "AdminSiteTypes", new { siteTypeId = resultModel.Id });
                }
                catch (Exception ex)
                {

                    this.logger.LogWarning($"{nameof(AdminSiteTypesController)} : {nameof(CreateSiteType)} : Exception - {ex.Message}");

                    return RedirectToAction("Error", "Home", new { message = "Can't Create site types. Contact support" });
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Type(string siteTypeId)
        {
            //display single site type
            //options for add widgets from created widgets
            try
            {
                var serviceCall = await this.siteTypeUsebleWidgetsService.GetSiteTypeAsync(siteTypeId);


                this.logger.LogInformation($"{nameof(AdminSiteTypesController)} : {nameof(Type)} : Geting administrated site type done.");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {

                this.logger.LogWarning($"{nameof(AdminSiteTypesController)} : {nameof(Type)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Get site types. Contact support" });
            }
                      
        }
        //CreateTemplate

    }
}
