using ApplicationCore.Entities.SiteType;
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

        public async Task CreateWidgetAsync(
            string name, string description, string functionality, string implementation,
            decimal price, int version, bool isOn, bool isFree, string widgetType, SiteWidgetEnum usebleWidgetType, string dependency,
            SiteTypesEnum siteType)
        {
            var newWidget = new Widget()
            {
                Dependency = 0,
                Description = description,
                Functionality = functionality,
                Implementation = implementation,
                IsFree = isFree,
                IsOn = isOn,
                Name = name,
                Price = price,
                SystemName = usebleWidgetType,
                Version = version,
                Votes = 0,
                SiteTypeSpecification = siteType,
            };

            await this.appWidgetRepository.AddAsync(newWidget);
        }

        public async Task<IEnumerable<Widget>> GetAllWidgetsAsync()
        {
            return await this.appWidgetRepository.ListAllAsync();
        }

        public IList<SiteWidgetEnum> GetBuildInWidgetTypes()
        {
            return new List<SiteWidgetEnum>()
            {
                SiteWidgetEnum.MenuDisplay,
                 SiteWidgetEnum.None,
                  SiteWidgetEnum.Products,
                   SiteWidgetEnum.SiteStructure,
                    SiteWidgetEnum.Testing,
                     SiteWidgetEnum.TopProducts
            };
        }

        public async Task<Widget> GetWidgetAsync(string widgetId)
        {
            return await this.appWidgetRepository.GetByIdAsync(widgetId);
        }
    }
}
