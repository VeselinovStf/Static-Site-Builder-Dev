using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppLaunchConfigService<T>
    {
        Task<T> GetSiteTypeLaunchConfig(string siteTypeId);

        Task LaunchSiteTypeLaunchConfig(string siteTypeId);

        Task UnLaunchSiteTypeLaunchConfig(string siteTypeId);

        Task PushSiteTypeLaunchConfig(string siteTypeId);
    }
}