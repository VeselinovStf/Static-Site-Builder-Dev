using ApplicationCore.Entities.BaseEntities;
using System.Collections.Generic;

namespace ApplicationCore.Entities.WidjetsEntity
{
    public class WidjetElement : DescriptiveEntity
    {
        public string Functionality { get; set; }

        public decimal Price { get; set; }

        public int Version { get; set; }

        public double Votes { get; set; }

        public bool IsOn { get; set; }

        public bool IsFree { get; set; }

        public ICollection<WidjetElement> ClientWidjets { get; set; }
    }
}