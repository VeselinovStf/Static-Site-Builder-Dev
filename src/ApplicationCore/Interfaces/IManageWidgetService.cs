using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IManageWidgetService<T>
    {
        Task<T> GetAllAsync(string clientId, string templateName);
    }
}
