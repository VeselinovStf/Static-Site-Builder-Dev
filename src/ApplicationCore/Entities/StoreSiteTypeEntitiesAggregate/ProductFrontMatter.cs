using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate
{
    public class ProductFrontMatter : BaseFrontMatter
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}