using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using Infrastructure.AdminSiteTypeWidgets.DTOs;
using Infrastructure.AdminSiteTypeWidgets.Exceptions;
using Infrastructure.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.AdminSiteTypeWidgets
{
    public class AdminSiteTypeWidgetsService : IAdminSiteTypeUsebleWidgetsService<UsebleSiteTypeWidgetListDTO>
    {
        private readonly IAppAdminSiteTypesUsebleWidgetsService<SiteType> appAdminSiteTypeUsebleWidgets;
        private readonly IAppWidgetService appWidgetService;

        public AdminSiteTypeWidgetsService(
            IAppAdminSiteTypesUsebleWidgetsService<SiteType> appAdminSiteTypeUsebleWidgets,
            IAppWidgetService appWidgetService)
        {
            this.appAdminSiteTypeUsebleWidgets = appAdminSiteTypeUsebleWidgets ?? throw new ArgumentNullException(nameof(appAdminSiteTypeUsebleWidgets));
            this.appWidgetService = appWidgetService ?? throw new ArgumentNullException(nameof(appWidgetService));
        }

        public async Task<UsebleSiteTypeWidgetListDTO> GetSiteTypeAsync(string siteTypeId)
        {
            try
            {
                var siteTypeCall = await this.appAdminSiteTypeUsebleWidgets.GetSiteTypeWithUsebleWidgetsAsync(siteTypeId);

                Validator.ObjectIsNull(
                 siteTypeCall, $"{nameof(AdminSiteTypeWidgetsService)} : {nameof(GetSiteTypeAsync)} : {nameof(siteTypeCall)} : Can't find site type!");

                var widgets = await this.appWidgetService.GetAllWidgetsAsync();

                Validator.ObjectIsNull(
                    widgets, $"{nameof(AdminSiteTypeWidgetsService)} : {nameof(GetSiteTypeAsync)} : {nameof(widgets)} : Can't find any widgets!");

                var usebleSiteTypeWidgets = widgets
                    .Where(w => w.SiteTypeSpecification.ToString() == siteTypeCall.Type.ToString());

                var usedIds = siteTypeCall.UsebleWidjets.Select(e => e.WidgetId);
                var buildInIds = usebleSiteTypeWidgets.Select(e => e.Id);
                var difference = buildInIds.Except(usedIds);

                var usebleWidgets = usebleSiteTypeWidgets
                    .Where(w => difference.Contains(w.Id));

                var returnModel = new UsebleSiteTypeWidgetListDTO()
                {
                    SiteTypeId = siteTypeId,
                    SiteTypeName = siteTypeCall.Name,
                    UsebleWidgets = new List<UsebleWidgetTypeDTO>(usebleWidgets.Select(w => new UsebleWidgetTypeDTO()
                    {
                        Functionality = w.Functionality,
                        Implementation = w.Implementation,
                        IsAdded = false,
                        IsFree = w.IsFree,
                        IsOn = w.IsOn,
                        Name = w.Name,
                        Price = w.Price,
                        Version = w.Version,
                        WidgetId = w.Id
                    }))
                };

                return returnModel;
            }
            catch (Exception ex)
            {

                throw new AdminSiteTypeWidgetsServiceGetSiteTypeException($"{nameof(AdminSiteTypeWidgetsServiceGetSiteTypeException)} : Can't get site type addeble widgets! : {ex.Message}");

            }
        }
    }
}
