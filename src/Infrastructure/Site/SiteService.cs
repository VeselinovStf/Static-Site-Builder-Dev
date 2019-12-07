using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Site.DTOs;
using Infrastructure.Site.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Site
{
    public class SiteService : ISiteService<SiteRenderingDTO>
    {
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService;
        private readonly IAppClientWidgetService appClientWidgetService;
        private readonly IAppProjectsService<Project> appProjectService;
        private readonly IAppBlogTypeSiteService<BlogTypeSite> appBlogTypeSiteService;
        private readonly IAppStoreTypeSiteService<StoreTypeSite> appStoreTypeSiteService;
        private readonly IAppWidgetService appWidgetService;

        public SiteService(IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService,
            IAppClientWidgetService appClientWidgetService,
            IAppProjectsService<Project> appProjectService,
            IAppBlogTypeSiteService<BlogTypeSite> appBlogTypeSiteService,
            IAppStoreTypeSiteService<StoreTypeSite> appStoreTypeSiteService,
            IAppWidgetService appWidgetService)
        {
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.appBlogTypeSiteService = appBlogTypeSiteService ?? throw new ArgumentNullException(nameof(appBlogTypeSiteService));
            this.appStoreTypeSiteService = appStoreTypeSiteService ?? throw new ArgumentNullException(nameof(appStoreTypeSiteService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
        }

        public async Task<SiteRenderingDTO> RenderSiteAsync(string clientId, string defaultStoreSiteTemplateName, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              defaultStoreSiteTemplateName, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(defaultStoreSiteTemplateName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                defaultStoreSiteTemplateName, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(defaultStoreSiteTemplateName)} : is null/empty");

            try
            {
                var usebleWidgetsCall = await this.appSiteTemplateService.GetTemplateAsync(defaultStoreSiteTemplateName);

                var clientProjectCall = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjectCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientProjectCall)} : {clientId} -> FATAL : Can't find project");

                var clientBlogProjectWidgetsId = clientProjectCall.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId).SiteUsedWidgets.Select(w => w.WidgetId).ToList();
                var clientStoreProjectWidgetsId = clientProjectCall.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId).SiteUsedWidgets.Select(w => w.WidgetId).ToList();
                var usebleWidgetsId = usebleWidgetsCall.SiteType.UsebleWidjets.Select(w => w.WidgetId).ToList();

                var widgetsCompareResultCall = await this.appWidgetService.GetAllWidgetsAsync();

                var clientBlogProjectWidgets = widgetsCompareResultCall.Where(x => clientBlogProjectWidgetsId.Contains(x.Id));
                var clientStoreProjectWidgets = widgetsCompareResultCall.Where(x => clientStoreProjectWidgetsId.Contains(x.Id));
                var usebleWidgets = widgetsCompareResultCall.Where(x => usebleWidgetsId.Contains(x.Id));


                var siteWidgets = new List<Widget>();
              
                siteWidgets.AddRange(usebleWidgets.Except(clientBlogProjectWidgets));
                siteWidgets.AddRange(usebleWidgets.Except(clientStoreProjectWidgets));


                var serviceModel = new SiteRenderingDTO()
                {
                    Widget = new List<WidgetsDTO>(siteWidgets.Select(s => new WidgetsDTO()
                    {
                        Name = s.Name
                    })
                )
                };
                

                return serviceModel;
            }
            catch (Exception ex)
            {

                throw new SiteServiceRenderSiteException($"{nameof(SiteServiceRenderSiteException)} : Can't render user site! : {ex.Message}");
            }
        }

        public async Task UpdateSiteWidgetsAsync(string clientId, string defaultStoreSiteTemplateName, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              defaultStoreSiteTemplateName, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(defaultStoreSiteTemplateName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                defaultStoreSiteTemplateName, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(defaultStoreSiteTemplateName)} : is null/empty");

            try
            {
                var usebleWidgetsCall = await this.appSiteTemplateService.GetTemplateAsync(defaultStoreSiteTemplateName);

                Validator.ObjectIsNull(
                 usebleWidgetsCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(usebleWidgetsCall)} : {defaultStoreSiteTemplateName} -> FATAL : Can't find template useble widgets");

                var clientWidgetsCall = await this.appClientWidgetService.GetAllAsync(clientId);

                Validator.ObjectIsNull(
                    clientWidgetsCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientWidgetsCall)} : {clientId} -> FATAL : Can't find client widgets");

                var usebleWidgetsId = usebleWidgetsCall.SiteType.UsebleWidjets.Select(w => w.WidgetId).ToList();
                var clientWidgetsId = clientWidgetsCall.ClientWidgets.Select(w => w.WidgetId).ToList();

                //compare for new free widgets
                var widgetsCompareResultIds = usebleWidgetsId
                    .Except(clientWidgetsId) .ToList();

                var widgetsCompareResultCall = await this.appWidgetService.GetAllWidgetsAsync();

                var widgetsCompareResultNewWidget = widgetsCompareResultCall.Where(w => widgetsCompareResultIds.Contains(w.Id));

                var allNewWidgets = widgetsCompareResultCall.Where(w => clientWidgetsId.Contains(w.Id)).Where(w => usebleWidgetsId.Contains(w.Id));

                allNewWidgets.ToList().AddRange(widgetsCompareResultNewWidget.ToList());

                //add
                var clientProjectCall = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjectCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientProjectCall)} : {clientId} -> FATAL : Can't find project");

                //return
                var clientBlogProject = clientProjectCall.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreProject = clientProjectCall.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

               // var clientNewWidgets = clientWidgetsCall.ClientWidgets.Add(widgetsCompareResult.Select(w => new ClientWidgets() {  }));

                if (clientBlogProject == null)
                {
                    if (clientStoreProject != null)
                    {
                        //storeProject add
                        await this.appStoreTypeSiteService.AddRangeOfWidgetsAsync(siteTypeId, allNewWidgets);
                    }
                    else
                    {
                        //throw
                        throw new ArgumentException($"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : NO SUCHE TEMPLATE :");
                    }
                }
                else
                {
                    //add to blogType by id
                    await this.appBlogTypeSiteService.AddRangeOfWidgetsAsync(siteTypeId, allNewWidgets);
                }

                
            }
            catch (Exception ex)
            {

                throw new SiteServiceUpdateSiteWidgetsAsyncException($"{nameof(SiteServiceUpdateSiteWidgetsAsyncException)} : Can't update site widgets! : {ex.Message}");
            }
        }
    }
}
