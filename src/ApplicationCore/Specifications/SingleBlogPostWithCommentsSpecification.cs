using ApplicationCore.Entities.PostAggregate;

namespace ApplicationCore.Specifications
{
    public class SingleBlogPostWithCommentsSpecification : BaseSpecification<Post>
    {
        public SingleBlogPostWithCommentsSpecification(string postId)
            : base(p => p.Id == postId)
        {
            Includes.Add(p => p.Comments);
        }
    }
}