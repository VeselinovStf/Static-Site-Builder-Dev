using Infrastructure.Identity;
using Web.ModelFatories.AccountManageModelFactory.Abstraction;
using Web.ViewModels.AccountManage;

namespace Web.ModelFatories.AccountManageModelFactory
{
    public class AccountManageModelFactory : IAccountManageModelFactory
    {
        public AccountManageClientInformationViewModel Create(ApplicationUser model)
        {
            return new AccountManageClientInformationViewModel()
            {
                UserId = model.Id,
                Delete = new AccountManageDeleteViewModel()
                {
                    UserId = model.Id
                },
                UserInformation = new AccountManageUserInformationViewModel()
                {
                    UserId = model.Id,
                    Email = model.Email,
                    UserName = model.UserName
                },
                Payments = new AccountManagePaymentViewModel()
                {
                    UserId = model.Id
                },
                Password = new AccountManagePasswordViewModel()
                {
                    UserId = model.Id
                }
            };
        }
    }
}