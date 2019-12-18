using System.Collections.Generic;

namespace Web.ViewModels.SiteType
{
    public class SiteTypeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string BuildInName { get; set; }

        public decimal Price { get; set; }

        public List<SiteTypeWidgetViewModel> SiteTypeWidgets { get; set; }
    }
}