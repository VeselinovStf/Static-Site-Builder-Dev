using ApplicationCore.Entities.SitesTemplates;

namespace ApplicationCore.Specifications
{
    public class SiteTemplatesByBuildInTypeNameSpecification : BaseSpecification<SiteTemplate>
    {
        public SiteTemplatesByBuildInTypeNameSpecification(string buildInType)
            : base(p => p.SiteType.Type.ToString() == buildInType)
        {
            AddInclude(p => p.SiteTemplateElements);
        }
    }
}