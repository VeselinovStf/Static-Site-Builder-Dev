using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.Site
{
    public class SiteViewModel
    {
        public string ClientId { get; set; }

        public IList<SiteWidgetViewModel> Widgets { get; set; }
    }
}
