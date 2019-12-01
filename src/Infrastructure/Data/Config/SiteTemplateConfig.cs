using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class SiteTemplateConfig : IEntityTypeConfiguration<SiteTemplate>
    {
        public void Configure(EntityTypeBuilder<SiteTemplate> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(SiteTemplate.SiteTemplateElements));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}