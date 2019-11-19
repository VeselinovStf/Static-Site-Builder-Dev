using ApplicationCore.Entities.SiteProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            var navigationStore = builder.Metadata.FindNavigation(nameof(Project.StoreSiteTypes));
            navigationStore.SetPropertyAccessMode(PropertyAccessMode.Field);

            var navigationBlog = builder.Metadata.FindNavigation(nameof(Project.BlogSiteTypes));
            navigationStore.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}