using ApplicationCore.Entities.SitesTemplates;
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

        public ManageWidgetsService(
            IAppClientWidgetService appClientWidgetService,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService)
        {
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
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


                var usebleWidgetsCall = await this.appSiteTemplateService.GetTemplateAsync(templateName);

                Validator.ObjectIsNull(
                    usebleWidgetsCall, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(usebleWidgetsCall)} : Can't find template");

                var usebleWidgetsId = usebleWidgetsCall.SiteType.UsebleWidjets.Select(w => w.WidgetId).ToList();

                var clientUsebleWidgets = clientWidgets
                    .ClientWidgets
                    .Where(w => usebleWidgetsId.Contains(w.WidgetId));

                Validator.ObjectIsNull(
                    clientUsebleWidgets, $"{nameof(ManageWidgetsService)} : {nameof(GetAllAsync)} : {nameof(clientUsebleWidgets)} : Can't find useble widgets");


                var widgets = clientUsebleWidgets.Select(w => w.Widget).ToList();


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
    }
}
