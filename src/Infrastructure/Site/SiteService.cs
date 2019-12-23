using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.SiteType;
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
        private const string SITE_RENDERING_DOMAIN = ".netlify.com";

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
            IAppWidgetService appWidgetService,
            IAppSiteTypesService<SiteType> appSiteTypeService)
        {
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.appBlogTypeSiteService = appBlogTypeSiteService ?? throw new ArgumentNullException(nameof(appBlogTypeSiteService));
            this.appStoreTypeSiteService = appStoreTypeSiteService ?? throw new ArgumentNullException(nameof(appStoreTypeSiteService));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
            
        }

        public async Task<SiteRenderingDTO> RenderSiteAsync(string clientId, string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
               clientId, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(clientId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              siteTypeId, $"{nameof(SiteService)} : {nameof(RenderSiteAsync)} : {nameof(siteTypeId)} : is null/empty");

          
            try
            {
                 var clientProjectCall = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjectCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientProjectCall)} : {clientId} -> FATAL : Can't find project");

                var widgetsCompareResultCall = await this.appWidgetService.GetAllWidgetsAsync();

                var serviceModel = new SiteRenderingDTO()
                {
                    ClientId = clientId,
                  
                };
                var siteWidgets = new List<Widget>();
             
                //return
                var clientBlogProject = clientProjectCall.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreProject = clientProjectCall.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                var siteAddress = string.Empty;
                if (clientBlogProject == null)
                {
                    if (clientStoreProject != null)
                    {
                        //GEt from StoreSiteTypes
                        var usebleWidgetsCall = await this.appStoreTypeSiteService.GetTypeWithUsedWidgetsSite(siteTypeId);
                        var usebleWidgetsId = usebleWidgetsCall.SiteUsedWidgets.Select(w => w.WidgetId).ToList();
                        var usebleTemplateWidgets = widgetsCompareResultCall.Where(w => usebleWidgetsId.Contains(w.Id));
                        var clientStoreProjectWidgetsId = clientProjectCall.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                        var clientStoreProjectWidgets = usebleTemplateWidgets.Where(x => clientStoreProjectWidgetsId.Id != x.Id);

                        //storeProject add
                        siteWidgets.AddRange(clientStoreProjectWidgets);
                        siteAddress = clientStoreProjectWidgetsId.Name;
                        serviceModel.TemplateName = usebleWidgetsCall.TemplateName;
                    }
                    else
                    {
                        //throw
                        throw new ArgumentException($"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : NO SUCHE TEMPLATE :");
                    }
                }
                else
                {
                    //GEt from BlogSiteTypes
                    var usebleWidgetsCall = await this.appBlogTypeSiteService.GetTypeWithUsedWidgetsSite(siteTypeId);
                    var usebleWidgetsId = usebleWidgetsCall.SiteUsedWidgets.Select(w => w.WidgetId).ToList();
                    var usebleTemplateWidgets = widgetsCompareResultCall.Where(w => usebleWidgetsId.Contains(w.Id));
                    var clientBlogProjectWidgetsId = clientProjectCall.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                    var clientBlogProjectWidgets = usebleTemplateWidgets.Where(x => clientBlogProjectWidgetsId.Id != x.Id);

                    //add to blogType by id
                    siteWidgets.AddRange(clientBlogProjectWidgets);
                    siteAddress = clientBlogProjectWidgetsId.Name;
                    serviceModel.TemplateName = usebleWidgetsCall.TemplateName;
                }

                //Add widgets -> usable
                serviceModel.PresentationLink = "https://" + siteAddress + SITE_RENDERING_DOMAIN;
                                                  
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
                //Get useble widgets for current template
                var templateUsableWidgets = await this.appSiteTemplateService.GetByTemplateNameAsync(defaultStoreSiteTemplateName);

                Validator.ObjectIsNull(
                 templateUsableWidgets, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(templateUsableWidgets)} : {defaultStoreSiteTemplateName} -> FATAL : Can't find template useble widgets");

                //Get client availible Widgets
                var clientAvailibleWidgetsCall = await this.appClientWidgetService.GetAllAsync(clientId);

                Validator.ObjectIsNull(
                    clientAvailibleWidgetsCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientAvailibleWidgetsCall)} : {clientId} -> FATAL : Can't find client widgets");

                //Useble id's'
                var usebleWidgetsId = templateUsableWidgets.SiteType.UsebleWidjets.Select(w => w.WidgetId).ToList();
                //Client id's
                var clientWidgetsId = clientAvailibleWidgetsCall.ClientWidgets.Select(w => w.WidgetId).ToList();

                //compare for new free widgets
                var widgetsNewCompareResultIds = usebleWidgetsId
                    .Except(clientWidgetsId).ToList();


                //All system widgets
                var systemWidgetsCall = await this.appWidgetService.GetAllWidgetsAsync();

                //Get new widget
                var newWidgets = systemWidgetsCall.Where(w => widgetsNewCompareResultIds.Contains(w.Id));
              

                var allNewWidgetsResult = new List<Widget>();


                allNewWidgetsResult.ToList().AddRange(newWidgets.ToList());

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
                        await this.appStoreTypeSiteService.AddRangeOfWidgetsAsync(siteTypeId, allNewWidgetsResult);
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
                    await this.appBlogTypeSiteService.AddRangeOfWidgetsAsync(siteTypeId, allNewWidgetsResult);
                }


            }
            catch (Exception ex)
            {

                throw new SiteServiceUpdateSiteWidgetsAsyncException($"{nameof(SiteServiceUpdateSiteWidgetsAsyncException)} : Can't update site widgets! : {ex.Message}");
            }
        }
    }
}
