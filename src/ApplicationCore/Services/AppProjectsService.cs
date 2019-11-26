using ApplicationCore.Entities.SiteProjectAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppProjectsService : IAppProjectsService<Project>
    {
        private readonly IAsyncRepository<Project> projectRepository;

        public AppProjectsService(
            IAsyncRepository<Project> projectRepository)
        {
            this.projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
        }

        public async Task<Project> GetClientProject(string clientId)
        {
            var specification = new ClientProjectWithSitesSpecification(clientId);

            return this.projectRepository.GetSingleBySpec(specification);
        }

        public async Task AddStoreTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            var specification = new ClientProjectWithStoreTypeSitesSpecification(clientId);

            var projectStores = this.projectRepository.GetSingleBySpec(specification);

            projectStores.AddStoreTypeSite(clientProjectId, name, description, clientId,
             buildInType, templateName,
             cardApiKey, cardServiceGate, hostingServiceGate,
             repository);

            await this.projectRepository.UpdateAsync(projectStores);
        }

        public async Task AddBlogTypeSite(string clientProjectId, string name, string description, string clientId,
            string buildInType, string templateName,
            string cardApiKey, string cardServiceGate, string hostingServiceGate,
            string repository)
        {
            var specification = new ClientProjectWithBlogTypeSitesSpecification(clientId);

            var projectStores = this.projectRepository.GetSingleBySpec(specification);

            projectStores.AddBlogTypeSite(clientProjectId, name, description, clientId,
             buildInType, templateName,
             cardApiKey, cardServiceGate, hostingServiceGate,
             repository);

            await this.projectRepository.UpdateAsync(projectStores);
        }
    }
}