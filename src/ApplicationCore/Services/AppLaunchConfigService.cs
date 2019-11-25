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

        public async Task AddHostingIdAsync(string siteTypeId, string hostHubId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            var config = this.launchConfigRepository.GetSingleBySpec(specification);

            config.HostingId = hostHubId;

            await this.launchConfigRepository.UpdateAsync(config);
        }

        public async Task AddRepositoryIdAsync(string siteTypeId, string createdHubId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            var config = this.launchConfigRepository.GetSingleBySpec(specification);

            config.RepositoryId = createdHubId;

            await this.launchConfigRepository.UpdateAsync(config);
        }

        public async Task<LaunchConfig> GetSiteTypeLaunchConfigAsync(string siteTypeId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            return this.launchConfigRepository.GetSingleBySpec(specification);
        }

        public async Task LaunchSiteTypeLaunchConfigAsync(string siteTypeId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            var config = this.launchConfigRepository.GetSingleBySpec(specification);

            config.IsLaunched = true;

            await this.launchConfigRepository.UpdateAsync(config);
        }

        public async Task PushSiteTypeLaunchConfigAsync(string siteTypeId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            var config = this.launchConfigRepository.GetSingleBySpec(specification);

            config.IsPushed = true;

            await this.launchConfigRepository.UpdateAsync(config);
        }

        public async Task UnLaunchSiteTypeLaunchConfigAsync(string siteTypeId)
        {
            var specification = new SiteTypeLaunchConfigSpecification(siteTypeId);

            var config = this.launchConfigRepository.GetSingleBySpec(specification);

            config.IsLaunched = false;

            await this.launchConfigRepository.UpdateAsync(config);
        }
    }
}