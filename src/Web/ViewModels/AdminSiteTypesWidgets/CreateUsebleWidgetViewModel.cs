using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.AdminSiteTypesWidgets
{
    public class CreateUsebleWidgetViewModel
    {
        public string SiteTypeId { get; set; }

        public string SiteTypeName { get; set; }

        public List<AddUsebleWidgetViewModel> UsebleWidgets { get; set; }

        public string WidgetId { get; set; }
    }
}
