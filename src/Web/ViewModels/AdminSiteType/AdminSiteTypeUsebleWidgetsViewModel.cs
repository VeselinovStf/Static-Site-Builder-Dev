using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.AdminSiteType
{
    public class AdminSiteTypeUsebleWidgetsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public IList<AdminSiteTemplateViewModel> SiteTemplates { get; set; }


        public IList<UsebleWidgetViewModel> UsebleWidgets { get; set; }
    }
}
