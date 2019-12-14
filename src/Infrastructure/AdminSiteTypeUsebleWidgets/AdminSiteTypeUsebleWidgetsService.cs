using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypeUsebleWidgets.DTOs;
using Infrastructure.AdminSiteTypeUsebleWidgets.Exceptions;
using Infrastructure.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AdminSiteTypeUsebleWidgets
{
    public class AdminSiteTypeUsebleWidgetsService : IAdminSiteTypeUsebleWidgetsService<AdminSiteTypeUsebleWidgetsDTO>
    {
        private readonly IAppAdminSiteTypesUsebleWidgetsService<SiteType> appAdminSiteTypeUsebleWidgets;
        private readonly IAppWidgetService appWidgetService;

        public AdminSiteTypeUsebleWidgetsService(
            IAppAdminSiteTypesUsebleWidgetsService<SiteType> appAdminSiteTypeUsebleWidgets,
            IAppWidgetService appWidgetService)
        {
            this.appAdminSiteTypeUsebleWidgets = appAdminSiteTypeUsebleWidgets ?? throw new ArgumentNullException(nameof(appAdminSiteTypeUsebleWidgets));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
        }

        public async Task AddUsebleWidgets(string siteTypeId, string widgetId)
        {
            Validator.StringIsNullOrEmpty(
               siteTypeId, $"{nameof(AdminSiteTypeUsebleWidgetsService)} : {nameof(AddUsebleWidgets)} : {nameof(siteTypeId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               widgetId, $"{nameof(AdminSiteTypeUsebleWidgetsService)} : {nameof(AddUsebleWidgets)} : {nameof(widgetId)} : is null/empty");

            try
            {

            }
            catch (Exception ex)
            {

                throw new AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException($"{nameof(AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException)} : Can't add widget! : {ex.Message}");

            }
        }

        public async Task<AdminSiteTypeUsebleWidgetsDTO> GetSiteTypeAsync(string siteTypeId)
        {
            Validator.StringIsNullOrEmpty(
                siteTypeId, $"{nameof(AdminSiteTypeUsebleWidgetsService)} : {nameof(GetSiteTypeAsync)} : {nameof(siteTypeId)} : is null/empty");

            try
            {
                var siteTypeCall = await this.appAdminSiteTypeUsebleWidgets.GetSiteTypeWithUsebleWidgetsAsync(siteTypeId);
                
                Validator.ObjectIsNull(
                 siteTypeCall, $"{nameof(AdminSiteTypeUsebleWidgetsService)} : {nameof(GetSiteTypeAsync)} : {nameof(siteTypeCall)} : Can't find site type!");

                var widgets = await this.appWidgetService.GetAllWidgetsAsync();
                
                Validator.ObjectIsNull(
                    widgets, $"{nameof(AdminSiteTypeUsebleWidgetsService)} : {nameof(GetSiteTypeAsync)} : {nameof(widgets)} : Can't find any widgets!");

                var usebleWidgets = widgets.Where(w => siteTypeCall.UsebleWidjets.Any(u => u.WidgetId == w.Id));

                var returnModel = new AdminSiteTypeUsebleWidgetsDTO()
                {
                    Id = siteTypeCall.Id,
                    Description = siteTypeCall.Description,
                    Name = siteTypeCall.Name,
                    UsebleWidgets = new List<UsebleWidgetDTO>(usebleWidgets.Select(w => new UsebleWidgetDTO()
                    {
                        Description = w.Description,
                        Functionality = w.Functionality,
                        Name = w.Name,
                        Id = w.Id
                    })),
                    SiteTemplates = new List<AdminSiteTemplateDTO>(siteTypeCall.SiteTemplates.Select(t => new AdminSiteTemplateDTO()
                    {
                        Name = t.Name,
                        Description = t.Description,
                        Id = t.Id
                    }))
                };


                return returnModel;
            }
            catch (Exception ex)
            {

                throw new AdminSiteTypeUsebleWidgetsServiceGetTypeException($"{nameof(AdminSiteTypeUsebleWidgetsServiceGetTypeException)} : Can't get site type! : {ex.Message}");

            }
        }
    }
}
