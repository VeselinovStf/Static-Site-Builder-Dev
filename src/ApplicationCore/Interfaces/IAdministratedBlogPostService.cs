using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdministratedBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllAdminPosts(string clientId);

        Task<string> Create(string header, string image, string content, string authorName);
    }
}