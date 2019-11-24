using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppLaunchConfigService<T>
    {
        Task<T> GetSiteTypeLaunchConfigAsync(string siteTypeId);

        Task LaunchSiteTypeLaunchConfigAsync(string siteTypeId);

        Task UnLaunchSiteTypeLaunchConfigAsync(string siteTypeId);

        Task PushSiteTypeLaunchConfigAsync(string siteTypeId);

        Task AddRepositoryIdAsync(string siteTypeId, string createdHubId);
    }
}