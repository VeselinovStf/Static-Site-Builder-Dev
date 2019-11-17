using ApplicationCore.Entities.PostAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppBlogPostService
    {
        Task<IEnumerable<Post>> GetAllPublicWithAuthorAsync();

        Task<IEnumerable<Post>> GetAllAdminWithCommentsAsync(string clientId);

        Task<Post> CreatePost(string header, string image, string content, string authorName, string id);

        Post GetSingleAsync(string id, string postId);

        Task<Post> EditPostAsync(string id, string postId, string header, string image, string content);

        Task RemovePost(string postId, string id);

        Task<Post> GetSinglePublicAsync(string postId);
    }
}