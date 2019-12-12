using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.AdminWidgets
{
    public class CreateWidgetViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Functionality { get; set; }

        public string Implementation { get; set; }
        public decimal Price { get; set; }

        public int Version { get; set; }

        public double Votes { get; set; }

        public bool IsOn { get; set; }

        public bool IsFree { get; set; }

        public string SiteType { get; set; }

        public IList<SelectListItem> SiteTypes { get; set; }


        public string UsebleWidgetType { get; set; }

        public IList<SelectListItem> UsebleWidgetTypes { get; set; }

        public string Dependency { get; set; }
    }
}
