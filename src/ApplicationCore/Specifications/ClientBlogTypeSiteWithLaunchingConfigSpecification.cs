using ApplicationCore.Entities.BlogSiteTypeEntities;

namespace ApplicationCore.Specifications
{
    public class ClientBlogTypeSiteWithLaunchingConfigSpecification : BaseSpecification<BlogTypeSite>
    {
        public ClientBlogTypeSiteWithLaunchingConfigSpecification(string clientID)
           : base(s => s.ClientId == clientID)
        {
            AddInclude(s => s.LaunchingConfig);
        }
    }
}