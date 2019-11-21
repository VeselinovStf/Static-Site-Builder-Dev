using System.Collections.Generic;

namespace Web.ViewModels.SiteType
{
    public class ClientSiteTypesViewModel
    {
        public string ClientId { get; set; }

        public IEnumerable<SiteTypeViewModel> SiteTypes { get; set; }
    }
}