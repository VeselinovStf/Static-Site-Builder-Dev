using ApplicationCore.Interfaces;
using Infrastructure.Blog.DTOs;
using Infrastructure.Blog.Exceptions;
using Infrastructure.Guard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Blog
{
    /// <summary>
    /// User Infrastructure Service to apply any bussiness rules, use Core Service for actual action
    /// </summary>
    public class BlogPostService : IBlogPostService<PublicPostDTO>
    {
        private readonly IAppBlogPostService appBlogPostService;

        public BlogPostService(
            IAppBlogPostService appBlogPostService)
        {
            this.appBlogPostService = appBlogPostService ?? throw new ArgumentNullException(nameof(appBlogPostService));
        }

        public async Task<IEnumerable<PublicPostDTO>> GetAllPublicPosts()
        {
            try
            {
                var publicPostCall = await this.appBlogPostService.GetAllWithAuthor();

                Validator.ObjectIsNull(
                  publicPostCall, $"{nameof(BlogPostService)} : {nameof(PublicPostDTO)} : {nameof(publicPostCall)} : Can't find any posts");

                var serviceModel = publicPostCall.Count() > 0 ? new List<PublicPostDTO>(publicPostCall.Select(p => new PublicPostDTO()
                {
                    Header = p.Header,
                    Image = p.Image,
                    PubDate = p.PubDate,
                    AuthorName = p.AuthorName,
                    Content = p.Content,
                    PostId = p.Id
                })) : new List<PublicPostDTO>();

                return serviceModel;
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetAllPublicPostsException($"{nameof(BlogPostServiceGetAllPublicPostsException)} : Can't get public post : {ex.Message}");
            }
        }
    }
}