using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities.StoreSiteTypeEntities
{
    public class ProductFrontMatter : BaseFrontMatter
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}