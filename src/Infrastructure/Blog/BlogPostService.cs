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
    public class BlogPostService :
        IPublicBlogPostService<PublicPostDTO>,
        IAdministratedBlogPostService<AdministratedPostDTO>,
        IClientBlogPostService<ClientPostDTO>
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

        public async Task<string> CreateAsync(string header, string image, string content, string authorName)
        {
            Validator.StringIsNullOrEmpty(
              header, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(header)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              image, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(image)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              content, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(content)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              authorName, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(authorName)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByUserNameAsync(authorName);

                Validator.ObjectIsNull(
                    client, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Administrator"))
                {
                    var createCall = await this.appBlogPostService.CreatePost(header, image, content, authorName, client.Id);

                    Validator.ObjectIsNull(
                        createCall, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(createCall)} : Can't create new post");

                    return createCall.AuthorId;
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(CreateAsync)} : -- CRYTICAL -- ADMINISTRATION USER EXCEPTION! : USER : {authorName}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceCreatePostException($"{nameof(BlogPostServiceCreatePostException)} : Can't Create Posts {ex.Message}");
            }
        }

        public async Task<string> DeletePostAsync(string postId, string authorName)
        {
            Validator.StringIsNullOrEmpty(
              postId, $"{nameof(BlogPostService)} : {nameof(DeletePostAsync)} : {nameof(postId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              authorName, $"{nameof(BlogPostService)} : {nameof(DeletePostAsync)} : {nameof(authorName)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByUserNameAsync(authorName);

                Validator.ObjectIsNull(
                    client, $"{nameof(BlogPostService)} : {nameof(CreateAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Administrator"))
                {
                    await this.appBlogPostService.RemovePost(postId, client.Id);

                    return client.Id;
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(CreateAsync)} : -- CRYTICAL -- ADMINISTRATION USER EXCEPTION! : USER : {authorName}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceCreatePostException($"{nameof(BlogPostServiceCreatePostException)} : Can't Create Posts {ex.Message}");
            }
        }

        public async Task<AdministratedPostDTO> EditPostAsync(string postId, string authorName, string header, string image, string content)
        {
            Validator.StringIsNullOrEmpty(
                postId, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(postId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                authorName, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(authorName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                header, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(header)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                image, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(image)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                content, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(content)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByUserNameAsync(authorName);

                Validator.ObjectIsNull(
                    client, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Administrator"))
                {
                    var postEditCall = await this.appBlogPostService.EditPostAsync(client.Id, postId, header, image, content);

                    Validator.ObjectIsNull(
                        postEditCall, $"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : {nameof(postEditCall)} : Can't edit post with this credidentials.");

                    return new AdministratedPostDTO()
                    {
                        AuthorName = postEditCall.AuthorName,
                        Comments = postEditCall.Comments.Select(c => new AdministratedCommentsDTO()
                        {
                            AuthorId = c.AuthorId,
                            PubDate = c.PubDate,
                            AuthorName = c.AuthorName,
                            Content = c.Content
                        }),
                        Content = postEditCall.Content,
                        Header = postEditCall.Header,
                        Image = postEditCall.Image,
                        PostId = postEditCall.Id,
                        PubDate = postEditCall.PubDate
                    };
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(EditPostAsync)} : -- CRYTICAL -- ADMINISTRATION USER EXCEPTION! : USER : {authorName}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceEditPostException($"{nameof(BlogPostServiceEditPostException)} : Can't Edit Post {ex.Message}");
            }
        }

        public async Task<IEnumerable<AdministratedPostDTO>> GetAllAdminPostsAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(BlogPostService)} : {nameof(GetAllAdminPostsAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                client, $"{nameof(BlogPostService)} : {nameof(GetAllAdminPostsAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Administrator"))
                {
                    var publicPostCall = await this.appBlogPostService.GetAllAdminWithCommentsAsync(clientId);

                    Validator.ObjectIsNull(
                      publicPostCall, $"{nameof(BlogPostService)} : {nameof(GetAllAdminPostsAsync)} : {nameof(publicPostCall)} : Can't find any posts");

                    var serviceModel = publicPostCall.Count() > 0 ? new List<AdministratedPostDTO>(publicPostCall.Select(p => new AdministratedPostDTO()
                    {
                        Header = p.Header,
                        Image = p.Image,
                        PubDate = p.PubDate,
                        AuthorName = p.AuthorName,
                        Content = p.Content,
                        PostId = p.Id,
                        Comments = p.Comments.Count() > 0 ? new List<AdministratedCommentsDTO>(p.Comments.Select(c => new AdministratedCommentsDTO()
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
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(GetAllAdminPostsAsync)} : -- CRYTICAL -- ADMINISTRATION USER EXCEPTION! : USER : {clientId}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetAllAdministratedPostsException($"{nameof(BlogPostServiceGetAllAdministratedPostsException)} : Can't get Administrated posts {ex.Message}");
            }
        }

        public async Task<IEnumerable<ClientPostDTO>> GetAllClientPostsWithCommentsAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(BlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                client, $"{nameof(BlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Client"))
                {
                    var publicPostCall = await this.appBlogPostService.GetAllClientPostsAsync();

                    Validator.ObjectIsNull(
                      publicPostCall, $"{nameof(BlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : {nameof(publicPostCall)} : Can't find any posts");

                    var serviceModel = publicPostCall.Count() > 0 ? new List<ClientPostDTO>(publicPostCall.Select(p => new ClientPostDTO()
                    {
                        Header = p.Header,
                        Image = p.Image,
                        PubDate = p.PubDate,
                        AuthorName = p.AuthorName,
                        Content = p.Content,
                        PostId = p.Id,
                        CommentsCount = p.Comments.Count()
                    })) : new List<ClientPostDTO>();

                    return serviceModel;
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : -- CRYTICAL -- CLIENT USER EXCEPTION! : USER : {clientId}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetAllClientPostsWithCommentsException($"{nameof(BlogPostServiceGetAllClientPostsWithCommentsException)} : Can't get Client posts {ex.Message}");
            }
        }

        public async Task<IEnumerable<PublicPostDTO>> GetAllPublicPostsAsync()
        {
            try
            {
                var publicPostCall = await this.appBlogPostService.GetAllPublicWithAuthorAsync();

                Validator.ObjectIsNull(
                  publicPostCall, $"{nameof(BlogPostService)} : {nameof(GetAllPublicPostsAsync)} : {nameof(publicPostCall)} : Can't find any posts");

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

        public async Task<AdministratedPostDTO> GetSinglePostAsync(string postId, string authorName)
        {
            Validator.StringIsNullOrEmpty(
            postId, $"{nameof(BlogPostService)} : {nameof(GetSinglePostAsync)} : {nameof(postId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             authorName, $"{nameof(BlogPostService)} : {nameof(GetSinglePostAsync)} : {nameof(authorName)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByUserNameAsync(authorName);

                Validator.ObjectIsNull(
                    client, $"{nameof(BlogPostService)} : {nameof(GetSinglePostAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Administrator"))
                {
                    var postCall = this.appBlogPostService.GetSingleAsync(client.Id, postId);

                    Validator.ObjectIsNull(
                        postCall, $"{nameof(BlogPostService)} : {nameof(GetSinglePostAsync)} : {nameof(postCall)} : Can't get post with this credidentials.");

                    return new AdministratedPostDTO()
                    {
                        AuthorName = postCall.AuthorName,
                        Comments = postCall.Comments.Select(c => new AdministratedCommentsDTO()
                        {
                            AuthorId = c.AuthorId,
                            PubDate = c.PubDate,
                            AuthorName = c.AuthorName,
                            Content = c.Content
                        }),
                        Content = postCall.Content,
                        Header = postCall.Header,
                        Image = postCall.Image,
                        PostId = postCall.Id,
                        PubDate = postCall.PubDate
                    };
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(BlogPostService)} : {nameof(GetSinglePostAsync)} : -- CRYTICAL -- ADMINISTRATION USER EXCEPTION! : USER : {authorName}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetSinglePostException($"{nameof(BlogPostServiceGetSinglePostException)} : Can't Get Post {ex.Message}");
            }
        }

        public async Task<PublicPostDTO> GetSinglePublicPostAsync(string postId)
        {
            Validator.StringIsNullOrEmpty(
           postId, $"{nameof(BlogPostService)} : {nameof(GetSinglePublicPostAsync)} : {nameof(postId)} : is null/empty");

            try
            {
                var postCall = await this.appBlogPostService.GetSinglePublicAsync(postId);

                Validator.ObjectIsNull(
                    postCall, $"{nameof(BlogPostService)} : {nameof(GetSinglePostAsync)} : {nameof(postCall)} : Can't get post with this credidentials.");

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