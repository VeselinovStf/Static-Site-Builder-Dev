using Infrastructure.Widgets.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.AdminWidgets;

namespace Web.ModelFatories.AdminWidgets.Abstraction
{
    public interface IAdminWidgetsModelFactory
    {
        AdminWidgetsListViewModel Create(AdminClientWidgetListDTO serviceCall);
    }
}
