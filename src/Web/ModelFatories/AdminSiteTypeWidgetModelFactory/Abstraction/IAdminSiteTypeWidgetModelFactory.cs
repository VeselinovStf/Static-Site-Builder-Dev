using Infrastructure.AdminSiteTypeWidgets.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.AdminSiteTypesWidgets;

namespace Web.ModelFatories.AdminSiteTypeWidgetModelFactory.Abstraction
{
    public interface IAdminSiteTypeWidgetModelFactory
    {
        CreateUsebleWidgetViewModel Create(UsebleSiteTypeWidgetListDTO serviceCall);
    }
}
