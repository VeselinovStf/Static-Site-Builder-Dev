using System.Collections.Generic;

namespace Web.ViewModels.Template
{
    public class SelectTemplateViewModel
    {
        public string ClientId { get; set; }

        public string BuildInSiteTypeName { get; set; }

        public string SiteTypeId { get; set; }
        public IList<SiteTemplateViewModel> SiteTypeTemplate { get; set; }
    }
}