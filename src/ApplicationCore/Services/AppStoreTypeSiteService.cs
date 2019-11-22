using ApplicationCore.Entities.StoreSiteTypeEntitiesAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppStoreTypeSiteService : IAppStoreTypeSiteService<StoreTypeSite>
    {
        private readonly IAsyncRepository<StoreTypeSite> storeTypeRepository;

        public AppStoreTypeSiteService(
            IAsyncRepository<StoreTypeSite> storeTypeRepository
            )
        {
            this.storeTypeRepository = storeTypeRepository;
        }

        public async Task DeleteClientStoreProjectAsync(string clientId)
        {
            var specification = new ClientStoreTypeSiteWithLaunchingConfigSpecification(clientId);

            var store = this.storeTypeRepository.GetSingleBySpec(specification);

            store.IsDeleted = true;

            await this.storeTypeRepository.UpdateAsync(store);
        }

        public async Task EditClientStoreProjectAsync(
            string clientId, string name, string description,
            string newProjectLocation, string templateLocation,
            string cardApiKey, string cardServiceGate,
            string hostingServiceGate, string repository)
        {
            var specification = new ClientStoreTypeSiteWithLaunchingConfigSpecification(clientId);

            var store = this.storeTypeRepository.GetSingleBySpec(specification);

            store.Name = name;
            store.Description = description;
            store.NewProjectLocation = newProjectLocation;
            store.TemplateLocation = templateLocation;
            store.LaunchingConfig.CardApiKey = cardApiKey;
            store.LaunchingConfig.CardServiceGate = cardServiceGate;
            store.LaunchingConfig.HostingServiceGate = hostingServiceGate;
            store.LaunchingConfig.Repository = repository;

            await this.storeTypeRepository.UpdateAsync(store);
        }
    }
}