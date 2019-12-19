using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels.SiteRendering;

namespace Web.ViewModels.Widgets.ProductWidget
{
    public class ProductsDisplayListViewModel
    {
        public string ProjectId { get; set; }

        public SiteRenderingViewModel SiteRendering { get; set; }

        public IList<ProductsSingleSimpleViewModel> Products { get; set; }
    }
}
