using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Widgets.DTOs
{
    public class AdminClientWidgetListDTO
    {
        public string ClientId { get; set; }

        public IList<WidgetDTO> ClientWidgets { get; set; }

     
    }
}
