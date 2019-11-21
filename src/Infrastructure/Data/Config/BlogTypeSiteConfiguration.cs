using ApplicationCore.Entities.BlogSiteTypeEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class BlogTypeSiteConfiguration : IEntityTypeConfiguration<BlogTypeSite>
    {
        public void Configure(EntityTypeBuilder<BlogTypeSite> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(BlogTypeSite.BlogPosts));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}