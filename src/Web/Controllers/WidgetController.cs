using ApplicationCore.Interfaces;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IManageWidgetService<ClientSiteWidgetsDTO> manageWidgetService;
        private readonly IWidgetModelFactory modelFactory;
        private readonly IAppLogger<WidgetController> logger;

        public WidgetController(
            IWidgetService<ClientWidgetListDTO> widgetService,
            IManageWidgetService<ClientSiteWidgetsDTO> manageWidgetService,
            IWidgetModelFactory modelFactory,
            IAppLogger<WidgetController> logger)
        {
            this.widgetService = widgetService ?? throw new ArgumentNullException(nameof(widgetService));
            this.manageWidgetService = manageWidgetService ?? throw new ArgumentNullException(nameof(manageWidgetService));
            this.modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(modelFactory));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Authorize(Roles = "Client")]
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

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<IActionResult> AddWidget(string widgetId, string clientId)
        {
            try
            {
                var result = await this.manageWidgetService.AddWidget(widgetId, clientId);

                if (result)
                {
                    this.logger.LogInformation($"{nameof(WidgetController)} : {nameof(AddWidget)} : Sucess - Adding Client Widgets");

                    return RedirectToAction("ManageClientWidgets", "Widget", new { clientId = clientId });
                }
                else
                {
                    this.logger.LogWarning($"{nameof(WidgetController)} : {nameof(AddWidget)} : Client tokens are to low to buy widget");

                    return RedirectToAction("IncefitionResourses", "Home", new { message = "You nead more credits to buy this widget" });
                }

            }
            catch (Exception ex)
            {
                this.logger.LogWarning($"{nameof(WidgetController)} : {nameof(AddWidget)} : Exception - {ex.Message}");

                return RedirectToAction("Error", "Home", new { message = "Can't add Client Widgets. Contact support" });
            }


        }

    }
}
