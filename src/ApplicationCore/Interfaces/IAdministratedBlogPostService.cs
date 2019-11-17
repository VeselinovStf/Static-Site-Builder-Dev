using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdministratedBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllAdminPosts(string clientId);

        Task<string> Create(string header, string image, string content, string authorName);

        Task<T> GetSinglePost(string postId, string authorName);

        Task<T> EditPost(string postId, string authorName, string header, string image, string content);

        Task<string> DeletePost(string postId, string authorName);
    }
}