using ApplicationCore.Interfaces;
using Infrastructure.Widgets.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Site;

namespace Web.Controllers
{
    [Authorize]
    public class SiteWidgetController : Controller
    {
        private readonly IManageSiteWidgetService manageWidgetService;

        public SiteWidgetController(
            IManageSiteWidgetService manageWidgetService)
        {
            this.manageWidgetService = manageWidgetService ?? throw new ArgumentNullException(nameof(manageWidgetService));
        }

        public async Task<IActionResult> Render(string widgetId, string clientId, string templateName, string siteTypeId)
        {
            try
            {            
                var confirmUsebility = await this.manageWidgetService.ConfirmUsebleWidget(widgetId, templateName);

                if (!string.IsNullOrWhiteSpace(confirmUsebility))
                {                 

                    return RedirectToAction(confirmUsebility, confirmUsebility + "Widget", new { widgetId = widgetId, clientId = clientId, templateName = templateName, siteTypeId = siteTypeId });
                
                }
                else
                {
                    return RedirectToAction("Error", "Home");
                }
               
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
