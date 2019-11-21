using ApplicationCore.Entities.SiteProjectAggregate;

namespace ApplicationCore.Specifications
{
    public class ClientProjectWithBlogTypeSitesSpecification : BaseSpecification<Project>
    {
        public ClientProjectWithBlogTypeSitesSpecification(string clientId)
            : base(p => p.ClientId == clientId)
        {
            AddInclude(p => p.BlogSiteTypes);
        }
    }
}