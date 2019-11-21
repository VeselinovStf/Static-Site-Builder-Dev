using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IClientProjectService<T>
    {
        Task<IEnumerable<T>> GetAllAsync(string clientId);
    }
}