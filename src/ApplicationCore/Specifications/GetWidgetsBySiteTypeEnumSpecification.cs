using ApplicationCore.Entities.WidjetsEntityAggregate;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class GetWidgetsBySiteTypeEnumSpecification : BaseSpecification<Widget>
    {
        public GetWidgetsBySiteTypeEnumSpecification(string buildInSiteType)
            : base(w => w.SiteTypeSpecification.ToString() == buildInSiteType)
        {
        }
    }
}
