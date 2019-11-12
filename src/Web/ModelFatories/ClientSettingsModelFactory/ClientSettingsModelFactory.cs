using Infrastructure.Identity;
using Web.ModelFatories.ClientSettingsModelFactory.Abstraction;
using Web.ViewModels.ClientSettings;

namespace Web.ModelFatories.ClientSettingsModelFactory
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