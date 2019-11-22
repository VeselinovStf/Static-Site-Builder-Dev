using ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{
    public class SiteTypeLaunchConfigSpecification : BaseSpecification<LaunchConfig>
    {
        public SiteTypeLaunchConfigSpecification(string siteTypeId)
            : base(l => l.SiteTypeId == siteTypeId)
        {
        }
    }
}