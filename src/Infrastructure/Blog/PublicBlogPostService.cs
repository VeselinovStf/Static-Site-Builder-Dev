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
    public class PublicBlogPostService : IPublicBlogPostService<PublicPostDTO>
    {
        private readonly IAppBlogPostService appBlogPostService;

        public PublicBlogPostService(
            IAppBlogPostService appBlogPostService)
        {
            this.appBlogPostService = appBlogPostService ?? throw new ArgumentNullException(nameof(appBlogPostService));
        }

        public async Task<IEnumerable<PublicPostDTO>> GetAllPublicPostsAsync()
        {
            try
            {
                var publicPostCall = await this.appBlogPostService.GetAllPublicWithAuthorAsync();

                Validator.ObjectIsNull(
                  publicPostCall, $"{nameof(PublicBlogPostService)} : {nameof(GetAllPublicPostsAsync)} : {nameof(publicPostCall)} : Can't find any posts");

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

        public async Task<PublicPostDTO> GetSinglePublicPostAsync(string postId)
        {
            Validator.StringIsNullOrEmpty(
           postId, $"{nameof(AdministratedBlogPostService)} : {nameof(GetSinglePublicPostAsync)} : {nameof(postId)} : is null/empty");

            try
            {
                var postCall = await this.appBlogPostService.GetSinglePublicAsync(postId);

                Validator.ObjectIsNull(
                    postCall, $"{nameof(PublicBlogPostService)} : {nameof(GetSinglePublicPostAsync)} : {nameof(postCall)} : Can't get post with this credidentials.");

                return new PublicPostDTO()
                {
                    AuthorName = postCall.AuthorName,
                    Content = postCall.Content,
                    Header = postCall.Header,
                    Image = postCall.Image,
                    PostId = postCall.Id,
                    PubDate = postCall.PubDate
                };
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetSinglePublicPostException($"{nameof(BlogPostServiceGetSinglePublicPostException)} : Can't Get Post {ex.Message}");
            }
        }
    }
}