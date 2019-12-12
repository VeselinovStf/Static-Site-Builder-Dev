using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.AdminSiteTypeUsebleWidgets.DTOs
{
    public class AdminSiteTypeUsebleWidgetsDTO
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }


        public IList<AdminSiteTemplateDTO> SiteTemplates { get; set; }

        public IList<UsebleWidgetDTO> UsebleWidgets { get; set; }
    }
}
