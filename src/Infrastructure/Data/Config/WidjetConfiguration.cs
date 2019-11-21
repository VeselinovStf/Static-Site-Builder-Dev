using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class WidjetConfiguration : IEntityTypeConfiguration<Widjet>
    {
        public void Configure(EntityTypeBuilder<Widjet> builder)
        {
            builder.Property(p => p.Price)
               .HasColumnType("decimal(5, 2)");
        }
    }
}