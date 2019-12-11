using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppAdminSiteTypesService : IAppAdminSiteTypesService<SiteType>
    {
        private readonly IAsyncRepository<SiteType> siteTypeRepository;

        public AppAdminSiteTypesService(IAsyncRepository<SiteType> siteTypeRepository)
        {
            this.siteTypeRepository = siteTypeRepository ?? throw new ArgumentNullException(nameof(siteTypeRepository));
        }

        public async Task<SiteType> CreateSiteTypeAsync(string name, string description, SiteTypesEnum type)
        {
            var createdEntity = await this.siteTypeRepository.AddAsync(new SiteType()
            {
                
                Name = name,
                Description = description,
                Type = type
            });

            return createdEntity;
        }

        public async Task<IEnumerable<SiteType>> GetAllAsync()
        {
            return await this.siteTypeRepository.ListAllAsync();
        }

        public IList<SiteTypesEnum> GetSiteTypes()
        {
            return new List<SiteTypesEnum>() { SiteTypesEnum.BlogType, SiteTypesEnum.StoreType };
        }
    }
}
