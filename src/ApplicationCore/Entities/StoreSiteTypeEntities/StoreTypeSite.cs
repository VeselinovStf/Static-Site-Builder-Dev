using ApplicationCore.Entities.BaseEntities;
using System.Collections.Generic;

namespace ApplicationCore.Entities.StoreSiteTypeEntities
{
    public class StoreTypeSite : BaseSiteProject
    {
        public ICollection<Product> Products { get; set; }
    }
}