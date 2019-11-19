using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities.BlogTypeSiteEntitiesAggregate
{
    public class BlogPostFrontMatter : BaseFrontMatter
    {
        public string BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}