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
      
    }
}