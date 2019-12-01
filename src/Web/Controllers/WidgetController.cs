using ApplicationCore.Interfaces;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.WidgetsModelFactory.Abstraction;

namespace Web.Controllers
{
    public class WidgetController : Controller
    {
        private readonly IWidgetService<ClientWidgetListDTO> widgetService;
        private readonly IWidgetModelFactory modelFactory;
        private readonly IAppLogger<WidgetController> logger;

        public WidgetController(
            IWidgetService<ClientWidgetListDTO> clientWidgetService,
            IWidgetModelFactory modelFactory,
            IAppLogger<WidgetController> logger)
        {
            this.widgetService = widgetService ?? throw new ArgumentNullException(nameof(widgetService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        public async Task<IActionResult> ManageClientWidgets(string clientId)
        {
            try
            {
                var serviceCall = await this.widgetService.GetAllAsync(clientId);

                this.logger.LogInformation($"{nameof(WidgetController)} : {nameof(ManageClientWidgets)} : Sucess - Getting Client Widgets");

                var model = this.modelFactory.Create(serviceCall);

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(WidgetController)} : {nameof(ManageClientWidgets)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't display Client Widgets. Contact support" });
            }
        }
    }
}
