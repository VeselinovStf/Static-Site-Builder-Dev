using ApplicationCore.Entities.SitesTemplates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class SiteTemplateByNameWithWidgetsSpecification : BaseSpecification<SiteTemplate>
    {
        public SiteTemplateByNameWithWidgetsSpecification(string templateName)
            :base(s => s.Name == templateName)
        {
            AddInclude(s => s.SiteType);
            AddInclude(s => s.SiteType.UsebleWidjets);
            
        }
    }
}
