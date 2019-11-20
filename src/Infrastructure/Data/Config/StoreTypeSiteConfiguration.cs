using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class StoreTypeSiteConfiguration : IEntityTypeConfiguration<StoreTypeSite>
    {
        public void Configure(EntityTypeBuilder<StoreTypeSite> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(StoreTypeSite.Products));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasBaseType<BaseSiteProject>();
        }
    }
}