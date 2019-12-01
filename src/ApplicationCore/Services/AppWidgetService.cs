using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppWidgetService : IAppWidgetService
    {
        private readonly IAsyncRepository<Widjet> appWidgetRepository;

        public AppWidgetService(
            IAsyncRepository<Widjet> appWidgetRepository)
        {
            this.appWidgetRepository = appWidgetRepository ?? throw new ArgumentNullException(nameof(appWidgetRepository));
        }
        public async Task<IEnumerable<Widjet>> GetAllWidgetsAsync()
        {
            return await this.appWidgetRepository.ListAllAsync();
        }
    }
}
