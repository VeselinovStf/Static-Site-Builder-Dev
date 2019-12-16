using ApplicationCore.Entities.SitesTemplates;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class GetTemplateByNameSpecification : BaseSpecification<SiteTemplate>
    {
        public GetTemplateByNameSpecification(string templateName)
            : base(t => t.Name == templateName)
        {
        }
    }
}
