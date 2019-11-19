using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class WidjetElementConfiguration : IEntityTypeConfiguration<WidjetElement>
    {
        public void Configure(EntityTypeBuilder<WidjetElement> builder)
        {
            builder.Property(p => p.Price)
               .HasColumnType("decimal(5, 2)");
        }
    }
}