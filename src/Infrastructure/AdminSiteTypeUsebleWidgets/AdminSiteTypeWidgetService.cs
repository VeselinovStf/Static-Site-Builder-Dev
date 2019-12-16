using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypeUsebleWidgets.Exceptions;
using Infrastructure.Guard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AdminSiteTypeUsebleWidgets
{
    public class AdminSiteTypeWidgetService : IAdminSiteTypeWidgetService
    {
     
        private readonly IAppSiteTypesService<SiteType> appSiteTypeService;
        private readonly IAppWidgetService appWidgetService;

        public AdminSiteTypeWidgetService(
            
            IAppSiteTypesService<SiteType> appSiteTypeService,
            IAppWidgetService appWidgetService)
        {
          
            this.appSiteTypeService = appSiteTypeService ?? throw new ArgumentNullException(nameof(appSiteTypeService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
        }
        public async Task AddUsebleWidgets(string siteTypeId, string widgetId)
        {
            Validator.StringIsNullOrEmpty(
               siteTypeId, $"{nameof(AdminSiteTypeWidgetService)} : {nameof(AddUsebleWidgets)} : {nameof(siteTypeId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               widgetId, $"{nameof(AdminSiteTypeWidgetService)} : {nameof(AddUsebleWidgets)} : {nameof(widgetId)} : is null/empty");

            try
            {
                var siteTypeCall = await this.appSiteTypeService.GetSiteTypeAsync(siteTypeId);

                Validator.ObjectIsNull(
                    siteTypeCall, $"{nameof(AdminSiteTypeWidgetService)} : {nameof(AddUsebleWidgets)} : {nameof(siteTypeCall)} : Can't find site type!");

                var widgetCall = await this.appWidgetService.GetWidgetAsync(widgetId);

                Validator.ObjectIsNull(
                   widgetCall, $"{nameof(AdminSiteTypeWidgetService)} : {nameof(AddUsebleWidgets)} : {nameof(widgetCall)} : Can't find widget!");

                if (siteTypeCall.Type.ToString() == widgetCall.SiteTypeSpecification.ToString())
                {
                    await this.appSiteTypeService.AddWidgetAsync(siteTypeId, widgetId);
                }
                else
                {
                    throw new AdminSiteTypeUsebleWidgetsServiceSiteTypeWidgetIsNotCompatableException("Site type and Widget are not compattable!");
                }
            }
            catch (Exception ex)
            {

                throw new AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException($"{nameof(AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException)} : Can't add widget! : {ex.Message}");

            }
        }
    }
}
