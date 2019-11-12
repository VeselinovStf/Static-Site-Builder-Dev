using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.Account
{
    public class AccountLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public bool IsInvalid { get; set; }
        public string ErrorMessage { get; set; }
    }
}