using ApplicationCore.Interfaces;
using Infrastructure.Identity;
using Infrastructure.SiteTypes.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.ModelFatories.SiteTypeModelFactory.Abstraction;
using Web.ViewModels.SiteType;

namespace Web.Controllers
{
    [Authorize]
    public class SiteTypeController : Controller
    {
        private readonly ISiteTypesService<SiteTypeDTO> siteTypesService;
        private readonly ISiteTypeEditorService<SiteTypeEditorDTO> siteTypeEditorService;
        private readonly IAccountService<ApplicationUser> accountService;
        private readonly ISiteTypeModelFactory modelFactory;
        private readonly IAppLogger<SiteTypeController> logger;

        public SiteTypeController(
            ISiteTypesService<SiteTypeDTO> siteTypesService,
            ISiteTypeEditorService<SiteTypeEditorDTO> siteTypeEditorService,
            IAccountService<ApplicationUser> accountService,
            ISiteTypeModelFactory modelFactory,
            IAppLogger<SiteTypeController> logger)
        {
            this.siteTypesService = siteTypesService ?? throw new ArgumentNullException(nameof(siteTypesService));
            this.siteTypeEditorService = siteTypeEditorService ?? throw new ArgumentNullException(nameof(siteTypeEditorService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
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

        /// <summary>
        /// Creates model for creating new Site Type
        /// Checking if incomming data is usable and is legal
        /// </summary>
        /// <param name="clientId">Id of user</param>
        /// <param name="buildInType">Build in type passed as string from selected site type</param>
        /// <returns>View with model for inputing user info for new project site type</returns>
        [HttpGet]
        public async Task<IActionResult> Create(string clientId, string buildInType)
        {
            try
            {
                if (await this.accountService.FindByIdAsync(clientId) != null)
                {
                    if (await this.siteTypesService.ConfirmTypeAsync(buildInType))
                    {
                        this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(SelectSiteType)} : Sucess - Getting Site Types");

                        var model = new CreateSiteTypeViewModel()
                        {
                            ClientId = clientId,
                            BuildInType = buildInType
                        };

                        return View(model);
                    }
                    else
                    {
                        this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(SelectSiteType)} :Create Site Type Confirm Type ERROR -- User : {clientId} : is doing something strange!!!");
                    }
                }
                else
                {
                    this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(SelectSiteType)} :Create Site Type Confirm Type ERROR -- User : {clientId} : is doing something strange, and is not presed in DB");
                }
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(SelectSiteType)} : Exception - {ex.Message}");
            }

            return RedirectToAction("Error", "Home", new { message = "Can't display site types. Contact support" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name","Description","ClientId",
            "BuildInType", "NewProjectLocation", "TemplateLocation",
            "CardApiKey", "CardServiceGate", "HostingServiceGate",
            "Repository")]
            CreateSiteTypeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.siteTypesService.CreateAsync(
                        model.Name, model.Description, model.ClientId,
                        model.BuildInType, model.NewProjectLocation, model.TemplateLocation,
                        model.CardApiKey, model.CardServiceGate, model.HostingServiceGate,
                        model.Repository);

                    this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(Create)} : Sucess - Creating Site Type");

                    return RedirectToAction("Index", "Projects", new { clientId = model.ClientId });
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(Create)} : Exception - {ex.Message}");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Site(string clientId, string siteTypeId)
        {
            try
            {
                var serviceCall = await this.siteTypeEditorService.GetClientEditableSiteTypeAsync(clientId, siteTypeId);

                this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(Site)} : Sucess - Getting Client Site Type");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(Site)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display client site type. Contact support" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditSite(string clientId, string siteTypeId)
        {
            try
            {
                var serviceCall = await this.siteTypeEditorService.GetClientEditableSiteTypeAsync(clientId, siteTypeId);

                this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(EditSite)} : Sucess - Getting Client Site Type");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(EditSite)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display client site type. Contact support" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSite([Bind("Name","Description","ClientId", "Id",
            "BuildInType", "NewProjectLocation", "TemplateLocation",
            "CardApiKey", "CardServiceGate", "HostingServiceGate",
            "Repository")]SiteTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.siteTypeEditorService.EditSiteTypeAsync(
                        model.Name, model.Description, model.ClientId,
                        model.Id, model.NewProjectLocation, model.TemplateLocation,
                        model.CardApiKey, model.CardServiceGate, model.HostingServiceGate,
                        model.Repository);

                    this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(EditSite)} : Success - Editing Client Site Type");

                    return RedirectToAction("Site", "SiteType", new { clientId = model.ClientId, siteTypeId = model.Id });
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(EditSite)} : Exception - {ex.Message}");

                    return RedirectToAction("Error", "Home", new { message = "Can't edit client site type. Contact support" });
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteSite(string clientId, string siteTypeId)
        {
            try
            {
                var serviceCall = await this.siteTypeEditorService.GetClientEditableSiteTypeAsync(clientId, siteTypeId);

                this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(DeleteSite)} : Sucess - Getting Client Site Type");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(DeleteSite)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display client site type. Contact support" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSite([Bind("ClientId", "Id")]SiteTypeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await this.siteTypeEditorService.DeleteSiteTypeAsync(model.ClientId, model.Id);

                    this.logger.LogInformation($"{nameof(SiteTypeController)} : {nameof(DeleteSite)} : Sucess - DFeletition Client Site Type");

                    return RedirectToAction("Index", "Projects", new { clientId = model.ClientId });
                }
                catch (Exception ex)
                {
                    this.logger.LogWarning($"{nameof(SiteTypeController)} : {nameof(DeleteSite)} : Exception - {ex.Message}");

                    return RedirectToAction("Error", "Home", new { message = "Can't delete client site type. Contact support" });
                }
            }

            return View(model);
        }
    }
}