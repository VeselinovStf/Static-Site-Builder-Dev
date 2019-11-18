using ApplicationCore.Entities.BaseEntities;
using ApplicationCore.Entities.BlogTypeSiteEntities;
using System.Collections.Generic;

namespace ApplicationCore.Entities.BlogSiteTypeEntities
{
    public class BlogTypeSite : BaseSiteProject
    {
        public ICollection<BlogPost> BlogPosts { get; set; }
    }
}