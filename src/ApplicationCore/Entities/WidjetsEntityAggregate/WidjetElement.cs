using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities.WidjetsEntityAggregate
{
    public class WidjetElement : DescriptiveEntity
    {
        public string Functionality { get; set; }

        public decimal Price { get; set; }

        public int Version { get; set; }

        public double Votes { get; set; }

        public bool IsOn { get; set; }

        public string Key { get; set; }

        public bool IsFree { get; set; }

        public string ClientWidjetId { get; private set; }

        public ClientWidjet ClientWidjet { get; set; }

        public string AvailibleSiteWidjetId { get; private set; }

        public BaseSiteProject AvailibleSiteWidjet { get; set; }

        public string UsedSiteWidjetId { get; private set; }

        public BaseSiteProject UsedSiteWidjet { get; set; }
    }
}