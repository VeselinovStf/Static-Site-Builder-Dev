using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
   public  class SiteWidgetConfiguration : IEntityTypeConfiguration<SiteWidget>
    {
        public void Configure(EntityTypeBuilder<SiteWidget> builder)
        {
            builder.HasKey(m => new { m.SiteId, m.WidgetId });

          

            builder.HasOne(pt => pt.Widget)
              .WithMany(p => p.SiteWidgets)
              .HasForeignKey(pt => pt.WidgetId);

        }
    }
}
