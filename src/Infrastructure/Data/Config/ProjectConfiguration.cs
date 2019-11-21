using ApplicationCore.Entities.SiteProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasMany(b => b.AvailibleWidjets)
               .WithOne(w => w.AvailibleSiteWidjet)
               .HasForeignKey(w => w.AvailibleSiteWidjetId);

            builder.HasMany(b => b.UsedWidjets)
               .WithOne(w => w.UsedSiteWidjet)
               .HasForeignKey(w => w.UsedSiteWidjetId);

            var elementMetadata = builder.Metadata.FindNavigation(nameof(Project.BlogSiteTypes));
            elementMetadata.SetField("_blogSiteTypes");
            elementMetadata.SetPropertyAccessMode(PropertyAccessMode.Field);

            //Work's only for one field!!
            var navigationStore = builder.Metadata.FindNavigation(nameof(Project.StoreSiteTypes));
            navigationStore.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}