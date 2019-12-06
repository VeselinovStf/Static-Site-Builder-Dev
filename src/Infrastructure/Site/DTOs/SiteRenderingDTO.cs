using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Site.DTOs
{
    public class SiteRenderingDTO
    {
        public ICollection<WidgetsDTO> Widget { get; set; }
    }
}
