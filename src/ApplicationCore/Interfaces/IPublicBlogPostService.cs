using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPublicBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllPublicPosts();

        Task<T> GetSinglePublicPost(string postId);
    }
}