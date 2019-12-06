using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class TemplateUsebleWidget
    {
        public string WidgetId { get; set; }

        public Widget Widget { get; set; }

        public string SiteProjectID { get; set; }

        public IBaseSiteProject SiteProject { get; set; }
    }
}
