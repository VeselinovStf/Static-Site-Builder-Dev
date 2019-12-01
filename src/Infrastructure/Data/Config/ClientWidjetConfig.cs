using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class ClientWidjetConfig : IEntityTypeConfiguration<ClientWidjet>
    {
        public void Configure(EntityTypeBuilder<ClientWidjet> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(ClientWidjet.ClientWidjets));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}