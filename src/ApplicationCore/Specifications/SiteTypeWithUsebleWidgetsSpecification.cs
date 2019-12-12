using ApplicationCore.Entities.SiteType;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class SiteTypeWithUsebleWidgetsSpecification : BaseSpecification<SiteType>
    {
        public SiteTypeWithUsebleWidgetsSpecification(string siteTypeId)
         : base(t => t.Id == siteTypeId)
        {
            AddInclude(t => t.UsebleWidjets);
            AddInclude(t => t.SiteTemplates);
        }
    }
}
