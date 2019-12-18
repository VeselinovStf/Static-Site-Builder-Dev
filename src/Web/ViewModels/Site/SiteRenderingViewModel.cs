using System.Collections.Generic;

namespace Web.ViewModels.Site
{
    public class SiteRenderingViewModel
    {
        public string ClientId { get; set; }

        public string SiteTypeId { get; set; }
        public string PresentationLink { get; set; }

        public string TemplateName { get; set; }
    }
}