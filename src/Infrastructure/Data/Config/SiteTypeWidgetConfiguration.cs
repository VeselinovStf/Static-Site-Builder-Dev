using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class SiteTypeWidgetConfiguration : IEntityTypeConfiguration<SiteTypeWidget>
    {
        public void Configure(EntityTypeBuilder<SiteTypeWidget> builder)
        {
            builder.HasKey(m => new { m.WidgetId, m.SiteTypeId});

            builder.HasOne(pt => pt.Widget)
                .WithMany(p => p.BuildInSiteTypeWidjets)
                .HasForeignKey(pt => pt.WidgetId);

            builder.HasOne(pt => pt.SiteType)
                .WithMany(t => t.UsebleWidjets)
                .HasForeignKey(pt => pt.SiteTypeId);
        }
    }
}
