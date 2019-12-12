using ApplicationCore.Interfaces;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.AdminWidgets.Abstraction;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminWidgetsController : Controller
    {
        private readonly IWidgetService<AdminClientWidgetListDTO> widgetsService;
        private readonly IAdminWidgetsModelFactory modelFactory;
        private readonly IAppLogger<AdminWidgetsController> logger;

        public AdminWidgetsController(
            IWidgetService<AdminClientWidgetListDTO> widgetsService,
            IAdminWidgetsModelFactory modelFactory,
            IAppLogger<AdminWidgetsController> logger)
        {
            this.widgetsService = widgetsService ?? throw new ArgumentNullException(nameof(widgetsService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
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
    }
}
