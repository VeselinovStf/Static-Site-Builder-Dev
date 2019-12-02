using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.Widget
{
    public class WidgetsListViewModel
    {
        public string ClientId { get; set; }

        public IEnumerable<WidgetViewModel> ClientWidgets { get; set; }

        public IEnumerable<WidgetViewModel> AvailibleWidgets { get; set; }
    }
}
