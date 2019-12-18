using ApplicationCore.Entities.SiteType;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class SiteTypesWithUsebleWidgetsSpecification : BaseSpecification<SiteType>
    {
        public SiteTypesWithUsebleWidgetsSpecification(bool isDeleted)
            : base(s => s.IsDeleted == isDeleted)
        {
            AddInclude(s => s.UsebleWidjets);
        }
    }
}
