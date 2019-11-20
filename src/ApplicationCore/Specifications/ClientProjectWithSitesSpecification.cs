using ApplicationCore.Entities.SiteProjectAggregate;

namespace ApplicationCore.Specifications
{
    public class ClientProjectWithSitesSpecification : BaseSpecification<Project>
    {
        public ClientProjectWithSitesSpecification(string clientId)
            : base(p => p.ClientId == clientId)
        {
            Includes.Add(p => p.StoreSiteTypes);
            Includes.Add(p => p.BlogSiteTypes);
        }
    }
}