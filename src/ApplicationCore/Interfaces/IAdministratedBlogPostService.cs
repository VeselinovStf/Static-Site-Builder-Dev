using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAdministratedBlogPostService<T>
    {
        Task<IEnumerable<T>> GetAllAdminPosts(string clientId);
    }
}