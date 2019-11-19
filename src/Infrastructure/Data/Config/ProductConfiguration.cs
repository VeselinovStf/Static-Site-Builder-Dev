using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Price)
                .HasColumnType("decimal(5, 2)");

            builder.Property(p => p.DiscountProcent)
              .HasColumnType("decimal(5, 2)");
        }
    }
}