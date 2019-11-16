using ApplicationCore.Entities.PostAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppBlogPostService
    {
        Task<IEnumerable<Post>> GetAllWithAuthor();
    }
}