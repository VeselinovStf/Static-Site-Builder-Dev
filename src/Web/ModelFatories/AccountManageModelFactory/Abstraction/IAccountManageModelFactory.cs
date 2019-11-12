using Infrastructure.Identity;
using Web.ViewModels.AccountManage;

namespace Web.ModelFatories.AccountManageModelFactory.Abstraction
{
    public interface IAccountManageModelFactory
    {
        AccountManageClientInformationViewModel Create(ApplicationUser model);
    }
}