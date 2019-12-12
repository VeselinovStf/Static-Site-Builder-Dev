using ApplicationCore.Entities.SiteType;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppAdminSiteTypesUsebleWidgetsService : IAppAdminSiteTypesUsebleWidgetsService<SiteType>
    {
        private readonly IAsyncRepository<SiteType> siteTypeRepository;

        public AppAdminSiteTypesUsebleWidgetsService(
            IAsyncRepository<SiteType> siteTypeRepository)
        {
            this.siteTypeRepository = siteTypeRepository ?? throw new ArgumentNullException(nameof(siteTypeRepository));
        }

        public async Task<SiteType> GetSiteTypeWithUsebleWidgetsAsync(string siteTypeId)
        {
            var specification = new SiteTypeWithUsebleWidgetsSpecification(siteTypeId);

            return  this.siteTypeRepository.GetSingleBySpec(specification);

        }
    }
}
