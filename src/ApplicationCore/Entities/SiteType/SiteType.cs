using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.SiteType
{
    public class SiteType : DescriptiveEntity , ISelingEntity
    {
        public SiteType()
        {
            this.SiteTemplates = new HashSet<SiteTemplate>();
            this.UsebleWidjets = new HashSet<SiteTypeWidget>();
        }

        public bool IsFree { get; set; }

        private decimal UnitPrice { get; set; }
        public decimal Price
        {
            get
            {
                if (this.IsFree)
                {
                    return 0m;
                }
                else
                {
                    return this.UnitPrice;
                }
            }
            set
            {
                this.UnitPrice = value;
            }
        }

        public SiteTypesEnum Type { get; set; }

        public ICollection<SiteTemplate> SiteTemplates { get; set; }

        public ICollection<SiteTypeWidget> UsebleWidjets { get; set; }


    }
}