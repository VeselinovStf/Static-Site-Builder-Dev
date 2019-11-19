using ApplicationCore.Entities.BaseEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BaseSiteConfiguration : IEntityTypeConfiguration<BaseSiteProject>
    {
        public void Configure(EntityTypeBuilder<BaseSiteProject> builder)
        {
            builder.HasMany(b => b.AvailibleWidjets)
                .WithOne(w => w.AvailibleSiteWidjet)
                .HasForeignKey(w => w.AvailibleSiteWidjetId);

            builder.HasMany(b => b.UsedWidjets)
               .WithOne(w => w.UsedSiteWidjet)
               .HasForeignKey(w => w.UsedSiteWidjetId);
        }
    }
}