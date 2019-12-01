using ApplicationCore.Entities.SiteProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            

            var elementMetadata = builder.Metadata.FindNavigation(nameof(Project.BlogSiteTypes));
            elementMetadata.SetField("_blogSiteTypes");
            elementMetadata.SetPropertyAccessMode(PropertyAccessMode.Field);

            //Work's only for one field!!
            var navigationStore = builder.Metadata.FindNavigation(nameof(Project.StoreSiteTypes));
            navigationStore.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}