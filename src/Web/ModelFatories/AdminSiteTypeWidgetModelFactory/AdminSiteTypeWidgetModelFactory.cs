using Infrastructure.AdminSiteTypeWidgets.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ModelFatories.AdminSiteTypeWidgetModelFactory.Abstraction;
using Web.ViewModels.AdminSiteTypesWidgets;

namespace Web.ModelFatories.AdminSiteTypeWidgetModelFactory
{
    public class AdminSiteTypeWidgetModelFactory : IAdminSiteTypeWidgetModelFactory
    {
        public CreateUsebleWidgetViewModel Create(UsebleSiteTypeWidgetListDTO serviceCall)
        {
            return new CreateUsebleWidgetViewModel()
            {
                SiteTypeId = serviceCall.SiteTypeId,
                SiteTypeName = serviceCall.SiteTypeName,
                UsebleWidgets = new List<AddUsebleWidgetViewModel>(serviceCall.UsebleWidgets.Select(w => new AddUsebleWidgetViewModel()
                {
                    Functionality = w.Functionality,
                    Implementation = w.Implementation,
                    IsAdded = false,
                    IsFree = w.IsFree,
                    IsOn = w.IsOn,
                    Name = w.Name,
                    Price = w.Price,
                    Version = w.Version,
                    WidgetId = w.WidgetId
                }))
            };
        }
    }
}
