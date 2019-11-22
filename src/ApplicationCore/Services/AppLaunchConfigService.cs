using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class AppLaunchConfigService : IAppLaunchConfigService<LaunchConfig>
    {
        private readonly IAsyncRepository<LaunchConfig> launchConfigRepository;

        public AppLaunchConfigService(
            IAsyncRepository<LaunchConfig> launchConfigRepository)
        {
            this.launchConfigRepository = launchConfigRepository ?? throw new ArgumentNullException(nameof(launchConfigRepository));
        }

        public async Task<LaunchConfig> GetSiteTypeLaunchConfig(string siteTypeId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            return this.launchConfigRepository.GetSingleBySpec(specification);
        }
    }
}