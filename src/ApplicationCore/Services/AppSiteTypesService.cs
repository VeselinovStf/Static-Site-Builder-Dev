using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
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

       

        public async Task<IEnumerable<SiteType>> GetAllAsync()
        {
            return await this.siteTypeRepository.ListAllAsync();
        }

       
    }
}