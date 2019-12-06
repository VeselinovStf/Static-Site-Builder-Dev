using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class StoreTypeSiteWithWidgetsSpecification : BaseSpecification<StoreTypeSite>
    {
        public StoreTypeSiteWithWidgetsSpecification(string typeId)
            :base(s => s.Id == typeId)
        {
            AddInclude(s => s.SiteUsedWidgets);
        }
    }
}
