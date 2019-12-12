using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.AdminWidgets
{
    public class AdminWidgetsListViewModel
    {
        public IEnumerable<AdminWidgetViewModel> AvailibleWidgets { get; set; }
    }
}
