using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class WidjetConfiguration : IEntityTypeConfiguration<Widget>
    {
        public void Configure(EntityTypeBuilder<Widget> builder)
        {
            builder.Property(p => p.Price)
               .HasColumnType("decimal(5, 2)");

            //builder.HasOne(s => s.UsebleSiteType)
            //    .WithMany(t => t.UsebleWidjets)
            //    .HasForeignKey(s => s.UsebleSiteTypeId);
        }
    }
}