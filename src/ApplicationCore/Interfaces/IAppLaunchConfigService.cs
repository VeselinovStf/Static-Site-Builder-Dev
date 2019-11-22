using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IAppLaunchConfigService<T>
    {
        Task<T> GetSiteTypeLaunchConfig(string siteTypeId);
    }
}