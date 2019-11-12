using Infrastructure.Identity;
using Web.ViewModels.ClientSettings;

namespace Web.ModelFatories.ClientSettings.Abstraction
{
    public interface IClientSettingsModelFactory
    {
        ClientSettingViewModel Create(ApplicationUser serviceModel);
    }
}