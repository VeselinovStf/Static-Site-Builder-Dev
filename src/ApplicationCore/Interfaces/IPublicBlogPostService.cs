using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IPublicBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllPublicPostsAsync();

        Task<T> GetSinglePublicPostAsync(string postId);
    }
}