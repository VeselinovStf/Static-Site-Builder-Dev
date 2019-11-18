using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdministratedBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllAdminPostsAsync(string clientId);

        Task<string> CreateAsync(string header, string image, string content, string authorName);

        Task<T> GetSinglePostAsync(string postId, string authorName);

        Task<T> EditPostAsync(string postId, string authorName, string header, string image, string content);

        Task<string> DeletePostAsync(string postId, string authorName);
    }
}