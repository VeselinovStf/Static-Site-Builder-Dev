namespace Web.ViewModels.AccountManage
{
    public class AccountManageClientInformationViewModel
    {
        public string UserId { get; set; }

        public AccountManageUserInformationViewModel UserInformation { get; set; }

        public AccountManagePasswordViewModel Password { get; set; }

        public AccountManagePaymentViewModel Payments { get; set; }

        public AccountManageDeleteViewModel Delete { get; set; }
    }
}