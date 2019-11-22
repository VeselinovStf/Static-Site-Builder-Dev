using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ILaunchSiteService
    {
        Task LaunchSite(string clientId, string siteTypeId);

        Task UnLaunchSite(string clientId, string siteTypeId);
    }
}