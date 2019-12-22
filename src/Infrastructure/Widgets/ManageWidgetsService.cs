using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Widgets.DTOs;
using Infrastructure.Widgets.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Widgets
{
    public class ManageWidgetsService : IManageWidgetService<ClientSiteWidgetsDTO>
    {
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService;
        private readonly IAppWidgetService appWidgetService;
        private readonly IAppWidgetCalculatorService widgetCalculator;

        public ManageWidgetsService(
            IAppClientWidgetService appClientWidgetService,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService,
            IAppWidgetService appWidgetService,
            IAppWidgetCalculatorService widgetCalculator)
        {
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
            this.widgetCalculator = widgetCalculator ?? throw new ArgumentNullException(nameof(widgetCalculator));
        }

        public async Task<bool> AddWidget(string widgetId, string clientId)
        {
            Validator.StringIsNullOrEmpty(
                  widgetId, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(widgetId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                 clientId, $"{nameof(ManageWidgetsService)} : {nameof(AddWidget)} : {nameof(clientId)} : is null/empty");

            try
            {
                var CanBuy = await this.widgetCalculator.TakeTokensAsync(clientId, widgetId);

                if (CanBuy)
                {
                    await this.appClientWidgetService.AddWidget(widgetId, clientId);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw new ManageWidgetsServiceAddWidgetException($"{nameof(ManageWidgetsServiceAddWidgetException)} : Exception : Can't add client widgets : {ex.Message}");

            }
        }

        public async Task CreateWidgetAsync(
            string name, string description, string functionality, string implementation, 
            decimal price, int version, bool isOn, bool isFree, string widgetType,
            string usebleWidgetType, string dependency)
        {
            Validator.StringIsNullOrEmpty(
               name, $"{nameof(ManageWidgetsService)} : {nameof(CreateWidgetAsync)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              description, $"{nameof(ManageWidgetsService)} : {nameof(CreateWidgetAsync)} : {nameof(description)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              functionality, $"{nameof(ManageWidgetsService)} : {nameof(CreateWidgetAsync)} : {nameof(functionality)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              implementation, $"{nameof(ManageWidgetsService)} : {nameof(CreateWidgetAsync)} : {nameof(implementation)} : is null/empty");

            try
            {
                var widgetTypeEnum = (SiteWidgetEnum)Enum.Parse(typeof(SiteWidgetEnum), usebleWidgetType);
                var type = (SiteTypesEnum)Enum.Parse(typeof(SiteTypesEnum), widgetType);

                await this.appWidgetService.CreateWidgetAsync( name,  description,  functionality,  implementation,
             price,  version,  isOn,  isFree,  widgetType, widgetTypeEnum,  dependency,
             type);
            }
            catch (Exception ex)
            {
                throw new ManageWidgetsServiceCreateWidgetException($"{nameof(ManageWidgetsServiceCreateWidgetException)} : Exception : Can't create client widgets : {ex.Message}");

            }
        }

        public async Task<ClientSiteWidgetsDTO> GetAllAsync(string clientId, string templateName)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               templateName, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(templateName)} : is null/empty");

            try
            {
                var clientWidgets = await this.appClientWidgetService.GetAllAsync(clientId);

                Validator.ObjectIsNull(
                    clientWidgets, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(clientWidgets)} : Can't find widgets with this client ID");


                var usebleWidgetsCall = await this.appSiteTemplateService.GetByTemplateNameAsync(templateName);

                Validator.ObjectIsNull(
                    usebleWidgetsCall, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(usebleWidgetsCall)} : Can't find template");

                var usebleWidgetsId = usebleWidgetsCall.SiteType.UsebleWidjets.Select(w => w.WidgetId).ToList();

                var clientUsebleWidgets = clientWidgets
                    .ClientWidgets
                    .Where(w => usebleWidgetsId.Contains(w.WidgetId));

                Validator.ObjectIsNull(
                    clientUsebleWidgets, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(clientUsebleWidgets)} : Can't find useble widgets");


                var usedWidgetsId = clientUsebleWidgets.Select(w => w.WidgetId).ToList();

                var systemWidgets = await this.appWidgetService.GetAllWidgetsAsync();

                var widgets = systemWidgets.Where(w => usedWidgetsId.Contains(w.Id));

                var resultModel = new ClientSiteWidgetsDTO()
                {
                    Widgets = new List<SiteWidgetDTO>(widgets.Select(w => new SiteWidgetDTO()
                    {
                        DisplayName = w.Name,
                        IsOn = w.IsOn,
                        WidgetId = w.Id
                    }))
                };


                return resultModel;
            }
            catch (Exception ex)
            {

                throw new ManageWidgetsServiceGetAllAsyncException($"{nameof(ManageWidgetsServiceGetAllAsyncException)} : Exception : Can't get client widgets : {ex.Message}");

            }
        }

        public IList<string> GetBuildInWidgetTypes()
        {

            try
            {
                var buildInWidgetTypes = this.appWidgetService.GetBuildInWidgetTypes();

                Validator.ObjectIsNull(
                    buildInWidgetTypes, $"{nameof(AdminWidgetsService)} : {nameof(GetBuildInWidgetTypes)} : {nameof(buildInWidgetTypes)} : Can't get build in widget types!");

                return new List<string>(buildInWidgetTypes.Select(t => t.ToString()));

            }
            catch (Exception ex)
            {

                throw new WidgetServiceGetBuildInWidgetTypesException($"{nameof(WidgetServiceGetBuildInWidgetTypesException)} : Can't get build in widget types! : {ex.Message}");

            }

        }
    }
}
