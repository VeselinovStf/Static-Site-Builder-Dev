using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SiteType
{
    public class SiteType : DescriptiveEntity
    {
        public SiteType()
        {
            this.SiteTemplates = new HashSet<SiteTemplate>();
            this.UsebleWidjets = new HashSet<SiteTypeWidget>();
        }
        public SiteTypesEnum Type { get; set; }

        public ICollection<SiteTemplate> SiteTemplates { get; set; }

        public ICollection<SiteTypeWidget> UsebleWidjets { get; set; }
    }
}