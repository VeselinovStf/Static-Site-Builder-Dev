using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppClientWidgetService : IAppClientWidgetService
    {
        private readonly IAsyncRepository<ApplicationUserWidgets> clientWidgetRepository;

        public AppClientWidgetService(
            IAsyncRepository<ApplicationUserWidgets> clientWidgetRepository)
            
        {
            this.clientWidgetRepository = clientWidgetRepository ?? throw new ArgumentNullException(nameof(clientWidgetRepository));
        }

        public async Task<ApplicationUserWidgets> GetAllAsync(string clientId)
        {
            var specification = new ClientWidgetsWithWidgetsSpecification(clientId);

            var result =  this.clientWidgetRepository.GetSingleBySpec(specification);

            return result;
        }
    }
}
