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

        public SiteService(IAppSiteTemplatesService<SiteTemplate> appSiteTemplateService,
            IAppClientWidgetService appClientWidgetService,
            IAppProjectsService<Project> appProjectService,
            IAppBlogTypeSiteService<BlogTypeSite> appBlogTypeSiteService,
            IAppStoreTypeSiteService<StoreTypeSite> appStoreTypeSiteService)
        {
            this.appSiteTemplateService = appSiteTemplateService ?? throw new ArgumentNullException(nameof(appSiteTemplateService));
            this.appClientWidgetService = appClientWidgetService ?? throw new ArgumentNullException(nameof(appClientWidgetService));
            this.appProjectService = appProjectService ?? throw new ArgumentNullException(nameof(appProjectService));
            this.appBlogTypeSiteService = appBlogTypeSiteService ?? throw new ArgumentNullException(nameof(appBlogTypeSiteService));
            this.appStoreTypeSiteService = appStoreTypeSiteService ?? throw new ArgumentNullException(nameof(appStoreTypeSiteService));
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

                var clientBlogProjectWidgets = clientProjectCall.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId).SiteUsedWidgets;
                var clientStoreProjectWidgets = clientProjectCall.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId).SiteUsedWidgets;

                var siteWidgets = new List<Widget>();

                siteWidgets.AddRange(usebleWidgetsCall.SiteType.UsebleWidjets.Except(clientStoreProjectWidgets));
                siteWidgets.AddRange(usebleWidgetsCall.SiteType.UsebleWidjets.Except(clientBlogProjectWidgets));


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

                var usebleWidgets = usebleWidgetsCall.SiteType.UsebleWidjets;
                var clientWidgets = clientWidgetsCall.ClientWidgets.Select(w => w.Widget).ToList();

                //compare for new free widgets
                var widgetsCompareResult = usebleWidgets
                    .Except(clientWidgets
                    .Select(w => w)
                    .Where(c => (c.IsFree) || (!c.IsDeleted && c.IsOn) ))
                    .ToList();

                clientWidgets.AddRange(widgetsCompareResult);

                //add
                var clientProjectCall = await this.appProjectService.GetClientProject(clientId);

                Validator.ObjectIsNull(
                    clientProjectCall, $"{nameof(SiteService)} : {nameof(UpdateSiteWidgetsAsync)} : {nameof(clientProjectCall)} : {clientId} -> FATAL : Can't find project");

                //return
                var clientBlogProject = clientProjectCall.BlogSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);
                var clientStoreProject = clientProjectCall.StoreSiteTypes.FirstOrDefault(b => b.Id == siteTypeId);

                if (clientBlogProject == null)
                {
                    if (clientStoreProject != null)
                    {
                        //storeProject add
                        await this.appStoreTypeSiteService.AddRangeOfWidgetsAsync(siteTypeId, clientWidgets);
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
                    await this.appBlogTypeSiteService.AddRangeOfWidgetsAsync(siteTypeId, clientWidgets);
                }

                
            }
            catch (Exception ex)
            {

                throw new SiteServiceUpdateSiteWidgetsAsyncException($"{nameof(SiteServiceUpdateSiteWidgetsAsyncException)} : Can't update site widgets! : {ex.Message}");
            }
        }
    }
}
