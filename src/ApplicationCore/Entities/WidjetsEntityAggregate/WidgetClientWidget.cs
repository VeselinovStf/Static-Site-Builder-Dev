using ApplicationCore.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class WidgetClientWidget : BaseEntity
    {
        public string WidgetId { get; set; }

        public string ClientWidgetId { get; set; }

        public Widjet Widget { get; set; }

        public ClientWidjet ClientWidget { get; set; }

    }
}
