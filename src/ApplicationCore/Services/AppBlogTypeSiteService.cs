using ApplicationCore.Entities.BlogSiteTypeEntities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppBlogTypeSiteService : IAppBlogTypeSiteService<BlogTypeSite>
    {
        private readonly IAsyncRepository<BlogTypeSite> blogTypeRepository;

        public AppBlogTypeSiteService(
            IAsyncRepository<BlogTypeSite> blogTypeRepository)
        {
            this.blogTypeRepository = blogTypeRepository ?? throw new ArgumentNullException(nameof(blogTypeRepository));
        }

        public async Task DeleteClientBlogProjectAsync(string clientId)
        {
            var specification = new ClientBlogTypeSiteWithLaunchingConfigSpecification(clientId);

            var blog = this.blogTypeRepository.GetSingleBySpec(specification);

            blog.IsDeleted = true;

            await this.blogTypeRepository.UpdateAsync(blog);
        }

        public async Task EditClientBlogProjectAsync(string clientId, string name, string description, string newProjectLocation, string templateLocation, string cardApiKey, string cardServiceGate, string hostingServiceGate, string repository)
        {
            var specification = new ClientBlogTypeSiteWithLaunchingConfigSpecification(clientId);

            var blog = this.blogTypeRepository.GetSingleBySpec(specification);

            blog.Name = name;
            blog.Description = description;
            blog.NewProjectLocation = newProjectLocation;
            blog.TemplateLocation = templateLocation;
            blog.LaunchingConfig.CardApiKey = cardApiKey;
            blog.LaunchingConfig.CardServiceGate = cardServiceGate;
            blog.LaunchingConfig.HostingServiceGate = hostingServiceGate;
            blog.LaunchingConfig.Repository = repository;

            await this.blogTypeRepository.UpdateAsync(blog);
        }
    }
}