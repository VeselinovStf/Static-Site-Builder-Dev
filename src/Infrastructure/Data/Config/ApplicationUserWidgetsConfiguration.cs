using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ApplicationUserWidgetsConfiguration : IEntityTypeConfiguration<ApplicationUserWidgets>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserWidgets> builder)
        {
            //var navigation = builder.Metadata.FindNavigation(nameof(ApplicationUserWidgets.ClientWidjets));
            //navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}