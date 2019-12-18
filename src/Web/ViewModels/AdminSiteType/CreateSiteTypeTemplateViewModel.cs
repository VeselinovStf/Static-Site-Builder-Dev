using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.AdminSiteType
{
    public class CreateSiteTypeTemplateViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string SiteType { get; set; }

        public decimal Price { get; set; }

        public List<SelectListItem> SiteTypes { get; set; }
    }
}
