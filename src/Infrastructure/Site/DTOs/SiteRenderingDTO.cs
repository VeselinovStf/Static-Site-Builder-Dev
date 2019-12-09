using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Site.DTOs
{
    public class SiteRenderingDTO
    {
        public string PresentationLink { get; set; }
        public string ClientId { get; set; }

        public string TemplateName { get; set; }
    }
}
