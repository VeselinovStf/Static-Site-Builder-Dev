using Infrastructure.Identity;
using Web.ModelFatories.ClientSettings.Abstraction;
using Web.ViewModels.ClientSettings;

namespace Web.ModelFatories.ClientSettings
{
    public class ClientSettingsModelFactory : IClientSettingsModelFactory
    {
        public ClientSettingViewModel Create(ApplicationUser serviceModel)
        {
            return new ClientSettingViewModel()
            {
                ClientId = serviceModel.Id
            };
        }
    }
}