using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.SiteType;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class Widjet : DescriptiveEntity
    {
        public string Functionality { get; set; }

        public decimal Price { get; set; }

        public int Version { get; set; }

        public double Votes { get; set; }

        public bool IsOn { get; set; }

        public string Key { get; set; }

        public bool IsFree { get; set; }

        public SiteTypesEnum SiteTypeSpecification { get; set; }

        public string UsebleSiteTypeId { get; set; }

        public SiteType.SiteType UsebleSiteType { get; set; }
    }
}