using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class WidgetClientWidgetConfiguration : IEntityTypeConfiguration<WidgetClientWidget>
    {
        public void Configure(EntityTypeBuilder<WidgetClientWidget> builder)
        {
            builder.HasKey(m => new { m.ClientWidgetId, m.WidgetId });

            builder.HasOne(pt => pt.ClientWidget)
                .WithMany(p => p.WidgetClientWidget)
                .HasForeignKey(pt => pt.ClientWidgetId);

            builder.HasOne(pt => pt.Widjet)
                .WithMany(t => t.WidgetClientWidget)
                .HasForeignKey(pt => pt.WidgetId);
        }
    }
}
