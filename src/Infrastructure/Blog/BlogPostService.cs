using ApplicationCore.Interfaces;
using Infrastructure.Blog.DTOs;
using Infrastructure.Blog.Exceptions;
using Infrastructure.Guard;
using Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Blog
{
    /// <summary>
    /// User Infrastructure Service to apply any bussiness rules, use Core Service for actual action
    /// </summary>
    public class BlogPostService : IPublicBlogPostService<PublicPostDTO>, IAdministratedBlogPostService<AdministratedPostDTO>
    {
        private readonly IAppBlogPostService appBlogPostService;
        private readonly IAccountService<ApplicationUser> accountService;

        public BlogPostService(
            IAppBlogPostService appBlogPostService,
            IAccountService<ApplicationUser> accountService)
        {
            this.appBlogPostService = appBlogPostService ?? throw new ArgumentNullException(nameof(appBlogPostService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<IEnumerable<AdministratedPostDTO>> GetAllAdminPosts(string clientId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(BlogPostService)} : {nameof(GetAllAdminPosts)} : {nameof(clientId)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                client, $"{nameof(BlogPostService)} : {nameof(GetAllAdminPosts)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Administrator"))
                {
                    var publicPostCall = await this.appBlogPostService.GetAllAdminWithCommentsAsync(clientId);

                    Validator.ObjectIsNull(
                      publicPostCall, $"{nameof(BlogPostService)} : {nameof(GetAllAdminPosts)} : {nameof(publicPostCall)} : Can't find any posts");

                    var serviceModel = publicPostCall.Count() > 0 ? new List<AdministratedPostDTO>(publicPostCall.Select(p => new AdministratedPostDTO()
                    {
                        Header = p.Header,
                        Image = p.Image,
                        PubDate = p.PubDate,
                        AuthorName = p.AuthorName,
                        Content = p.Content,
                        PostId = p.Id,
                        Comments = p.Content.Count() > 0 ? new List<AdministratedCommentsDTO>(p.Comments.Select(c => new AdministratedCommentsDTO()
                        {
                            AuthorName = c.AuthorName,
                            AuthorId = c.AuthorId,
                            Content = c.Content,
                            PubDate = c.PubDate
                        })) : new List<AdministratedCommentsDTO>()
                    })) : new List<AdministratedPostDTO>();

                    return serviceModel;
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(GetAllAdminPosts)} : -- CRYTICAL -- ADMINISTRATION USER EXCEPTION! : USER : {clientId}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetAllAdministratedPostsException($"{nameof(BlogPostServiceGetAllAdministratedPostsException)} : Can't get Administrated posts {ex.Message}");
            }
        }

        public async Task<IEnumerable<PublicPostDTO>> GetAllPublicPosts()
        {
            try
            {
                var publicPostCall = await this.appBlogPostService.GetAllPublicWithAuthorAsync();

                Validator.ObjectIsNull(
                  publicPostCall, $"{nameof(BlogPostService)} : {nameof(GetAllPublicPosts)} : {nameof(publicPostCall)} : Can't find any posts");

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