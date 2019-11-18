using ApplicationCore.Entities.BaseEntities;

namespace ApplicationCore.Entities.BlogTypeSiteEntities
{
    public class BlogPostFrontMatter : BaseFrontMatter
    {
        public string BlogPostId { get; set; }

        public BlogPost BlogPost { get; set; }
    }
}