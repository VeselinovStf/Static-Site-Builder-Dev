using ApplicationCore.Entities.PostAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Post.Comments));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}