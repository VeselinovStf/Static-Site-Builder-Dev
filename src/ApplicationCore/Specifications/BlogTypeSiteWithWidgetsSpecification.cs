using ApplicationCore.Entities.BlogSiteTypeEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class BlogTypeSiteWithWidgetsSpecification : BaseSpecification<BlogTypeSite>
    {
        public BlogTypeSiteWithWidgetsSpecification(string typeId)
            : base(s => s.Id == typeId)
        {
            AddInclude(s => s.SiteUsedWidgets);
        }
    }
}
