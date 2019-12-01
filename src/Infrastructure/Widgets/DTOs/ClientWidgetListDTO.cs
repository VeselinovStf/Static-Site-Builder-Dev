using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Widgets.DTOs
{
    public class ClientWidgetListDTO
    {
        public string ClientId { get; set; }

        public IList<WidgetDTO> ClientWidgets { get; set; }

        public IList<WidgetDTO> AvailibleWidgets { get; set; }
    }
}
