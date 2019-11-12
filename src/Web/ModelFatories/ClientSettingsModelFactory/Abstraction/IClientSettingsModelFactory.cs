using Infrastructure.Identity;
using Web.ViewModels.ClientSettings;

namespace Web.ModelFatories.ClientSettingsModelFactory.Abstraction
{
    public interface IClientSettingsModelFactory
    {
        ClientSettingViewModel Create(ApplicationUser serviceModel);
    }
}