using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class SiteWidget
    {
        public string WidgetId { get; set; }


        public Widget Widget { get; set; }

        public string SiteId { get; set; }

        public BaseWidget SiteProgect { get; set; }
    }
}
