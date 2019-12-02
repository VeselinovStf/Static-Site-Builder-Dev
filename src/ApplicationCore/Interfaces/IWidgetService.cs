using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IWidgetService<T>
    {
        Task<T> GetAllAsync(string clientId);
        Task<T> GetAllAdminAsync(string clientId);
    }
}
