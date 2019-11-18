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
    public class ClientBlogPostService : IClientBlogPostService<ClientPostDTO>
    {
        private readonly IAppBlogPostService appBlogPostService;
        private readonly IAccountService<ApplicationUser> accountService;

        public ClientBlogPostService(
            IAppBlogPostService appBlogPostService,
            IAccountService<ApplicationUser> accountService)
        {
            this.appBlogPostService = appBlogPostService ?? throw new ArgumentNullException(nameof(appBlogPostService));
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<IEnumerable<ClientPostDTO>> GetAllClientPostsWithCommentsAsync(string clientId)
        {
            Validator.StringIsNullOrEmpty(
                clientId, $"{nameof(AdministratedBlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : {nameof(clientId)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                client, $"{nameof(AdministratedBlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : {nameof(client)} : Can't find any client with this id");

                var roles = await this.accountService.GetRolesAsync(client);

                if (roles.Contains("Client"))
                {
                    var publicPostCall = await this.appBlogPostService.GetAllClientPostsAsync();

                    Validator.ObjectIsNull(
                      publicPostCall, $"{nameof(AdministratedBlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : {nameof(publicPostCall)} : Can't find any posts");

                    var serviceModel = publicPostCall.Count() > 0 ? new List<ClientPostDTO>(publicPostCall.Select(p => new ClientPostDTO()
                    {
                        Header = p.Header,
                        Image = p.Image,
                        PubDate = p.PubDate,
                        AuthorName = p.AuthorName,
                        Content = p.Content,
                        PostId = p.Id,
                        Comments = new List<ClientCommentsDTO>(p.Comments.Select(c => new ClientCommentsDTO()
                        {
                            AuthorId = c.AuthorId,
                            AuthorName = c.AuthorName,
                            Content = c.Content,
                            PubDate = c.PubDate
                        }))
                    })) : new List<ClientPostDTO>();

                    return serviceModel;
                }
                else
                {
                    throw new InvalidOperationException($"{nameof(AdministratedBlogPostService)} : {nameof(GetAllClientPostsWithCommentsAsync)} : -- CRYTICAL -- CLIENT USER EXCEPTION! : USER : {clientId}");
                }
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetAllClientPostsWithCommentsException($"{nameof(BlogPostServiceGetAllClientPostsWithCommentsException)} : Can't get Client posts {ex.Message}");
            }
        }

        public async Task<ClientPostDTO> GetPrivatePostWithComments(string postId)
        {
            Validator.StringIsNullOrEmpty(
           postId, $"{nameof(AdministratedBlogPostService)} : {nameof(GetPrivatePostWithComments)} : {nameof(postId)} : is null/empty");

            try
            {
                var postCall = await this.appBlogPostService.GetSingleWithComments(postId);

                Validator.ObjectIsNull(
                    postCall, $"{nameof(AdministratedBlogPostService)} : {nameof(GetPrivatePostWithComments)} : {nameof(postCall)} : Can't get post with this credidentials.");

                return new ClientPostDTO()
                {
                    AuthorName = postCall.AuthorName,
                    Content = postCall.Content,
                    Header = postCall.Header,
                    Image = postCall.Image,
                    PostId = postCall.Id,
                    PubDate = postCall.PubDate,
                    Comments = new List<ClientCommentsDTO>(postCall.Comments.Select(c => new ClientCommentsDTO()
                    {
                        AuthorId = c.AuthorId,
                        AuthorName = c.AuthorName,
                        Content = c.Content,
                        PubDate = c.PubDate
                    }))
                };
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceGetPrivatePostWithCommentsAsyncException($"{nameof(BlogPostServiceGetPrivatePostWithCommentsAsyncException)} : Can't Get Post {ex.Message}");
            }
        }

        public async Task CreateCommentAsync(string postId, string authorId, string authorName, string content)
        {
            Validator.StringIsNullOrEmpty(
               postId, $"{nameof(AdministratedBlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(postId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             authorId, $"{nameof(AdministratedBlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(authorId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             authorName, $"{nameof(AdministratedBlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(authorName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             content, $"{nameof(AdministratedBlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(content)} : is null/empty");

            try
            {
                var client = await this.accountService.FindByUserNameAsync(authorName);

                Validator.ObjectIsNull(
                    client, $"{nameof(AdministratedBlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(client)} : Can't find any client with this id");

                Validator.StringEqualsString(
                   client.Id, authorId, $"{nameof(AdministratedBlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(client)} is not {nameof(authorName)} : Can't find any client with this this author Name");

                //var post = await this.appBlogPostService.GetSingleAsync(client.Id, postId);

                //Validator.ObjectIsNull(
                //   post, $"{nameof(BlogPostService)} : {nameof(CreateCommentAsync)} : {nameof(post)} : Can't find any post with this id to add comment to");

                await this.appBlogPostService.AddCommentAsync(postId, client.Id, client.UserName, content, DateTime.Now);
            }
            catch (Exception ex)
            {
                throw new BlogPostServiceCreateCommentException($"{nameof(BlogPostServiceCreateCommentException)} : Can't Create Comment {ex.Message}");
            }
        }
    }
}