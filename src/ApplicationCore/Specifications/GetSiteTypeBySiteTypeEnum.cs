using ApplicationCore.Entities.SiteType;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class GetSiteTypeBySiteTypeEnum : BaseSpecification<SiteType>
    {
        public GetSiteTypeBySiteTypeEnum(string siteTypeEnum)
            :base(s => s.Type.ToString() == siteTypeEnum)
        {
        }
    }
}
