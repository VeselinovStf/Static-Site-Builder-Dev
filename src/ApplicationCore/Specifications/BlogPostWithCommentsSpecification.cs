using ApplicationCore.Entities.PostAggregate;

namespace ApplicationCore.Specifications
{
    public class BlogPostWithCommentsSpecification : BaseSpecification<Post>
    {
        public BlogPostWithCommentsSpecification(string clientId)
            : base(bp => bp.AuthorId == clientId)
        {
            Includes.Add(bp => bp.Comments);
        }
    }
}