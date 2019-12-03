using ApplicationCore.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class ClientWidgets 
    {
        public string WidgetId { get; set; }

        public string ApplicationUserWidgetsId { get; set; }

        public Widget Widget { get; set; }

        public ApplicationUserWidgets ApplicationUserWidgets { get; set; }

    }
}
