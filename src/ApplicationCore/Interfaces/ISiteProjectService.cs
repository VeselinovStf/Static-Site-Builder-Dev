using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteProjectService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string clientId);
    }
}