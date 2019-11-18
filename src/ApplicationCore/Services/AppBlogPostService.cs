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

        public async Task<Post> EditPostAsync(string clientId, string postId, string header, string image, string content)
        {
            var clientMailBoxSpec = new BlogPostWithCommentsSpecification(clientId);

            var post = this.blogPostRepository.GetSingleBySpec(clientMailBoxSpec);

            post.Header = header;
            post.Image = image;
            post.Content = content;

            await this.blogPostRepository.UpdateAsync(post);

            return post;
        }

        public async Task<IEnumerable<Post>> GetAllAdminWithCommentsAsync(string clientId)
        {
            var clientMailBoxSpec = new BlogPostWithCommentsSpecification(clientId);

            return await this.blogPostRepository.ListAsync(clientMailBoxSpec);
        }

        public async Task<IEnumerable<Post>> GetAllClientPostsAsync()
        {
            return await this.blogPostRepository.ListAllAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPublicWithAuthorAsync()
        {
            return await this.blogPostRepository.ListAllAsync();
        }

        //TODO: Update this getter
        public Post GetSingleAsync(string clientId, string postId)
        {
            var clientMailBoxSpec = new BlogPostWithCommentsSpecification(clientId);

            return this.blogPostRepository.GetSingleBySpec(clientMailBoxSpec);
        }

        public async Task<Post> GetSinglePublicAsync(string postId)
        {
            return await this.blogPostRepository.GetByIdAsync(postId);
        }

        public async Task RemovePost(string postId, string id)
        {
            var clientMailBoxSpec = new BlogPostWithCommentsSpecification(id);

            var post = this.blogPostRepository.GetSingleBySpec(clientMailBoxSpec);

            post.IsDeleted = true;

            await this.blogPostRepository.UpdateAsync(post);
        }
    }
}