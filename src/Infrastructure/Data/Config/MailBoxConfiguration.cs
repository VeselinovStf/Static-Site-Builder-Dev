using ApplicationCore.Entities.MessageAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class MailBoxConfiguration : IEntityTypeConfiguration<MailBox>
    {
        public void Configure(EntityTypeBuilder<MailBox> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(MailBox.Messages));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}