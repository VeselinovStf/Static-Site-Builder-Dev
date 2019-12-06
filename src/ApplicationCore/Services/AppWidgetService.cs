using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppWidgetService : IAppWidgetService
    {
        private readonly IAsyncRepository<Widget> appWidgetRepository;

        public AppWidgetService(
            IAsyncRepository<Widget> appWidgetRepository)
        {
            this.appWidgetRepository = appWidgetRepository ?? throw new ArgumentNullException(nameof(appWidgetRepository));
        }
        public async Task<IEnumerable<Widget>> GetAllWidgetsAsync()
        {
            return await this.appWidgetRepository.ListAllAsync();
        }

     
    }
}
