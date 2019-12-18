using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Widgets
{
    public class ManageSiteWidgetService : IManageSiteWidgetService
    {
        private readonly IAppWidgetService appWidgetService;

        public ManageSiteWidgetService(
            IAppWidgetService appWidgetService)
        {
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
        }

        //TODO: FIX THIS IMPLEMENTATION
        public async Task<string> ConfirmUsebleWidget(string widgetId, string templateName)
        {
            //Service - get widget name with provided widgetId 
            //var widgetName = await this.widgetService.Get(widgetId);
            //var confirmUsebility = await this.somettttt.ConfirmUsebleWidget(templateName);
            var widget = await this.appWidgetService.GetWidgetAsync(widgetId);

            return widget.Name;
        }
    }
}
