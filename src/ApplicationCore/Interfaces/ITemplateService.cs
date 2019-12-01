using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITemplateService<T>
    {
        Task<IList<T>> GetAllAsync(string buildInType, string clientId);

        
    }
}