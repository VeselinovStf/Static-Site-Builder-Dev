using ApplicationCore.Entities.PostAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
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

        public async Task<Post> CreatePost(string header, string image, string content, string authorName, string id)
        {
            var newPost = new Post()
            {
                AuthorId = id,
                AuthorName = authorName,
                Content = content,
                Header = header,
                Image = image,
                PubDate = DateTime.Now
            };

            var addedPost = await this.blogPostRepository.AddAsync(newPost);

            return addedPost;
        }

        public async Task<IEnumerable<Post>> GetAllAdminWithCommentsAsync(string clientId)
        {
            var clientMailBoxSpec = new BlogPostWithCommentsSpecification(clientId);

            return await this.blogPostRepository.ListAsync(clientMailBoxSpec);
        }

        public async Task<IEnumerable<Post>> GetAllPublicWithAuthorAsync()
        {
            return await this.blogPostRepository.ListAllAsync();
        }
    }
}