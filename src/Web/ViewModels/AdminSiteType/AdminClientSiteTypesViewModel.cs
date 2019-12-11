using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels.AdminSiteType
{
    public class AdminClientSiteTypesViewModel
    {
        public string ClientId { get; set; }

        public IEnumerable<AdminSiteTypeViewModel> SiteTypes { get; set; }
    }
}
