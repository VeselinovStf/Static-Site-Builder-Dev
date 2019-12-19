using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.SiteRendering
{
    public class SiteRenderingViewModel
    {
        public string ClientId { get; set; }

        public string SiteTypeId { get; set; }
        public string PresentationLink { get; set; }

        public string TemplateName { get; set; }
    }
}
