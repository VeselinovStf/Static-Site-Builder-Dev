using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteProjectAggregate;
using System.Collections.Generic;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class WidjetElement : BaseEntity
    {
        public ICollection<Widjet> Widjets { get; set; }

        public string ClientWidjetId { get; private set; }

        public ClientWidjet ClientWidjet { get; set; }

        public string AvailibleSiteWidjetId { get; private set; }

        public Project AvailibleSiteWidjet { get; set; }

        public string UsedSiteWidjetId { get; private set; }

        public Project UsedSiteWidjet { get; set; }
    }
}