using ApplicationCore.Entities.SitesTemplates;

namespace ApplicationCore.Specifications
{
    public class SiteTemplateWithElementsSpecification : BaseSpecification<SiteTemplate>
    {
        public SiteTemplateWithElementsSpecification(string templateName)
            : base(t => t.Name == templateName)
        {
            AddInclude(t => t.SiteTemplateElements);
        }
    }
}