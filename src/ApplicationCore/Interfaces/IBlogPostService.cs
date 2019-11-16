using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllPublicPosts();
    }
}