using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypes.DTOs;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.ModelFatories.AdminWidgets.Abstraction;
using Web.ViewModels.AdminWidgets;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminWidgetsController : Controller
    {
        private readonly IWidgetService<AdminClientWidgetListDTO> widgetsService;
        private readonly IManageWidgetService<ClientSiteWidgetsDTO> widgetManageService;
        private readonly IAdminSiteTypeService<AdminSiteTypeDTO> siteTypeService;
        private readonly IAdminWidgetsModelFactory modelFactory;
        private readonly IAppLogger<AdminWidgetsController> logger;

        public AdminWidgetsController(
            IWidgetService<AdminClientWidgetListDTO> widgetsService,
            IManageWidgetService<ClientSiteWidgetsDTO> widgetManageService,
            IAdminSiteTypeService<AdminSiteTypeDTO> siteTypeService,
            IAdminWidgetsModelFactory modelFactory,
            IAppLogger<AdminWidgetsController> logger)
        {
            this.widgetsService = widgetsService ?? throw new ArgumentNullException(nameof(widgetsService));
            this.widgetManageService = widgetManageService ?? throw new ArgumentNullException(nameof(widgetManageService));
            this.siteTypeService = siteTypeService ?? throw new ArgumentNullException(nameof(siteTypeService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        [HttpGet]
        public async Task<IActionResult> AdminWidgets(string clientId)
        {
            try
            {
                var serviceCall = await this.widgetsService.GetAllAsync(clientId);

                this.logger.LogInformation($"{nameof(AdminWidgetsController)} : {nameof(AdminWidgets)} : Sucess - Getting Admin Widgets");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminWidgetsController)} : {nameof(AdminWidgets)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display Admin Widgets. Contact support" });
            }


        }

        [HttpGet]
        public IActionResult Create()
        {           
            try
            {
                var buildInWidgetTypesCall = this.widgetManageService.GetBuildInWidgetTypes();

                var buildInSiteTypesCall = this.siteTypeService.GetBuildInSiteTypes();

                this.logger.LogInformation($"{nameof(AdminWidgetsController)} : {nameof(Create)} : Geting administrated useble widgets type done.");

                var model = this.modelFactory.Create(buildInWidgetTypesCall, buildInSiteTypesCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(AdminSiteTypesController)} : {nameof(Create)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't Get widget types types. Contact support" });
            }          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Name","Description","Functionality","Implementation",
                "Price","Version","IsOn","IsFree","SiteType","UsebleWidgetType","Dependency"
            )]CreateWidgetViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: Fix ID
                     await this.widgetManageService.CreateWidgetAsync(
                        model.Name,model.Description,model.Functionality,model.Implementation,
                        model.Price,model.Version,model.IsOn,model.IsFree,model.SiteType,model.UsebleWidgetType,model.Dependency
                        );

                    this.logger.LogInformation($"{nameof(AdminWidgetsController)} : {nameof(Create)} : Creating widget done.");

                    return RedirectToAction("AdminWidgets", "AdminWidgets", new { clientId = User.FindFirstValue(ClaimTypes.NameIdentifier) });
                }
                catch (Exception ex)
                {

                    this.logger.LogWarning($"{nameof(AdminWidgetsController)} : {nameof(Create)} : Exception - {ex.Message}");

                    return RedirectToAction("Error", "Home", new { message = "Can't Create widget. Contact support" });
                }
            }

            return View();
          
        }
    }
}
