using ApplicationCore.Entities.WidjetsEntityAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppClientWidgetService
    {
        Task<ClientWidjet> GetAllAsync(string clientId);
    }
}
