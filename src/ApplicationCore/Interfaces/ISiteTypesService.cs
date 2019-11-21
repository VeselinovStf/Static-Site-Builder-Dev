using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteTypesService<T>
    {
        Task<IEnumerable<T>> GetAllTypesAsync();
    }
}