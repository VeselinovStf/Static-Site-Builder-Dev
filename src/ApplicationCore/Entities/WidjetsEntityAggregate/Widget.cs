using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using System.Collections.Generic;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class Widget : DescriptiveEntity, ISelingEntity
    {
        public string Functionality { get; set; }

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
        public string Icon { get; set; }

        public int Version { get; set; }

        public double Votes { get; set; }

        public bool IsOn { get; set; }

        public string Key { get; set; }

        public bool IsFree { get; set; }

        public string Implementation { get; set; }

        public SiteTypesEnum SiteTypeSpecification { get; set; }

        public string UsebleSiteTypeId { get; set; }

        public SiteType.SiteType UsebleSiteType { get; set; }

        public SiteWidgetEnum SystemName { get; set; }
        public SiteWidgetEnum Dependency { get; set; }

        public ICollection<ClientWidgets> WidgetClientWidget { get; set; }
        public ICollection<SiteTypeWidget> BuildInSiteTypeWidjets { get; set; }
        public ICollection<SiteWidget> SiteWidgets { get; set; }


    }
}