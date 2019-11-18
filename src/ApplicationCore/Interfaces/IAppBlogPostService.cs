using ApplicationCore.Entities.PostAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppBlogPostService
    {
        Task<IEnumerable<Post>> GetAllPublicWithAuthorAsync();

        Task<IEnumerable<Post>> GetAllAdminWithCommentsAsync(string clientId);

        Task<Post> CreatePost(string header, string image, string content, string authorName, string id);

        Task<Post> GetSingleAsync(string clientId, string postId);

        Task<Post> EditPostAsync(string id, string postId, string header, string image, string content);

        Task RemovePost(string postId, string id);

        Task<Post> GetSinglePublicAsync(string postId);

        Task<IEnumerable<Post>> GetAllClientPostsAsync();

        Task<Post> GetSingleWithComments(string postId);

        Task AddCommentAsync(string postId, string authorId, string authorName, string content, DateTime pubDate);
    }
}