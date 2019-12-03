using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data.Config
{
    public class ClientWidgetConfiguration : IEntityTypeConfiguration<ClientWidgets>
    {
        public void Configure(EntityTypeBuilder<ClientWidgets> builder)
        {
            builder.HasKey(m => new { m.ApplicationUserWidgetsId, m.WidgetId });

            builder.HasOne(pt => pt.ApplicationUserWidgets)
                .WithMany(p => p.ClientWidgets)
                .HasForeignKey(pt => pt.ApplicationUserWidgetsId);

            builder.HasOne(pt => pt.Widget)
                .WithMany(t => t.WidgetClientWidget)
                .HasForeignKey(pt => pt.WidgetId);
        }
    }
}
