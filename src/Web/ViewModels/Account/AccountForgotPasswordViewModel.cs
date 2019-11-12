using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool IsSend { get; set; }
    }
}