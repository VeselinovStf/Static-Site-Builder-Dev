using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IClientBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllClientPostsWithCommentsAsync(string clientId);

        Task<T> GetPrivatePostWithComments(string postId);

        Task CreateCommentAsync(string postId, string authorId, string authorName, string content);
    }
}