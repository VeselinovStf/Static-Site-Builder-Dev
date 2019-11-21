using ApplicationCore.Entities.SiteProjectAggregate;

namespace ApplicationCore.Specifications
{
    public class ClientProjectWithStoreTypeSitesSpecification : BaseSpecification<Project>
    {
        public ClientProjectWithStoreTypeSitesSpecification(string clientId)
            : base(p => p.ClientId == clientId)
        {
            AddInclude(p => p.StoreSiteTypes);
        }
    }
}