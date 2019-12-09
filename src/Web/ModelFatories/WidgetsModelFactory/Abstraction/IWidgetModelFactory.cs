using Infrastructure.Widgets.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.Site;
using Web.ViewModels.Widget;

namespace Web.ModelFatories.WidgetsModelFactory.Abstraction
{
    public interface IWidgetModelFactory
    {
        WidgetsListViewModel Create(ClientWidgetListDTO inputModel);
        SiteViewModel Create(ClientSiteWidgetsDTO serviceCall);
    }
}
