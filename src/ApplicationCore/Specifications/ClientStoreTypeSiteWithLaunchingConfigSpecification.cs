using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;

namespace ApplicationCore.Specifications
{
    public class ClientStoreTypeSiteWithLaunchingConfigSpecification : BaseSpecification<StoreTypeSite>
    {
        public ClientStoreTypeSiteWithLaunchingConfigSpecification(string clientID)
            : base(s => s.ClientId == clientID)
        {
            AddInclude(s => s.LaunchingConfig);
        }
    }
}