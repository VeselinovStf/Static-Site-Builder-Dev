using ApplicationCore.Entities.SiteType;
using ApplicationCore.Entities.WidjetsEntityAggregate;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppSiteTypesService : IAppSiteTypesService<SiteType>
    {
        private readonly IAsyncRepository<SiteType> siteTypeRepository;
        

        public AppSiteTypesService(
            IAsyncRepository<SiteType> siteTypeRepository)
           
        {
            this.siteTypeRepository = siteTypeRepository ?? throw new ArgumentNullException(nameof(siteTypeRepository));
            
        }

        public async Task AddWidgetAsync(string siteTypeId, string widgetId)
        {
            var siteType = await this.siteTypeRepository.GetByIdAsync(siteTypeId);

            siteType.UsebleWidjets.Add(new SiteTypeWidget()
            {
                WidgetId = widgetId
            });

            await this.siteTypeRepository.UpdateAsync(siteType);
        }

     

        public async Task<IEnumerable<SiteType>> GetAllWithWidgetsAsync()
        {
            var specification = new SiteTypesWithUsebleWidgetsSpecification(false);

            return await this.siteTypeRepository.ListAsync(specification);
        }

        public async Task<SiteType> GetSiteTypeAsync(string siteTypeId)
        {
            return await this.siteTypeRepository.GetByIdAsync(siteTypeId);
        }
    }
}