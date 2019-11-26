using ApplicationCore.Entities.SitesTemplates;

namespace ApplicationCore.Specifications
{
    public class SiteTemplateByBuildNameAndTemplateNameSpecification : BaseSpecification<SiteTemplate>
    {
        public SiteTemplateByBuildNameAndTemplateNameSpecification(string buildInSiteType, string templateName)
            : base(s => s.Name == templateName && s.SiteType.Type.ToString() == buildInSiteType)
        {
            AddInclude(s => s.SiteTemplateElements);
        }
    }
}