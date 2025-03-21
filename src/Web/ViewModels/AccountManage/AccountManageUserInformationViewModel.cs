﻿using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels.AccountManage
{
    public class AccountManageUserInformationViewModel
    {
        public string UserId { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Current User Name")]
        public string UserName { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}