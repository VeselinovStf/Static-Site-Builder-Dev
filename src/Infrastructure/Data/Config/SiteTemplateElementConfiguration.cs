using ApplicationCore.Entities.SitesTemplates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class SiteTemplateElementConfiguration : IEntityTypeConfiguration<SiteTemplateElement>
    {
        public void Configure(EntityTypeBuilder<SiteTemplateElement> builder)
        {
            builder.Property(s => s.FileContent)
                .HasColumnType("VARCHAR(MAX)");
        }
    }
}