using ApplicationCore.Entities.PostAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    /// <summary>
    /// App Services - do basic validation of parameters and contact repository for getting the results
    /// </summary>
    public class AppBlogPostService : IAppBlogPostService
    {
        private readonly IAsyncRepository<Post> blogPostRepository;

        public AppBlogPostService(
            IAsyncRepository<Post> blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository ?? throw new ArgumentNullException(nameof(blogPostRepository));
        }

        public async Task<IEnumerable<Post>> GetAllWithAuthor()
        {
            return await this.blogPostRepository.ListAllAsync();
        }
    }
}