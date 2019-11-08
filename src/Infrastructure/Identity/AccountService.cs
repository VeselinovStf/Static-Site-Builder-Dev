using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Identity.DTOs;
using Infrastructure.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AccountService : IAccountService<ApplicationUser>
    {
        private readonly IAppUserManager<ApplicationUser> userManager;
        private readonly IAppSignInManager<ApplicationUser> signInManager;

        public AccountService(
            IAppUserManager<ApplicationUser> userManager,
            IAppSignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
        {
            Validator.ObjectIsNull(
                user, $"{nameof(AccountService)} : {nameof(GetRolesAsync)} : {nameof(user)} : object is null");

            try
            {
                return await this.userManager.GetRolesAsync(user);
            }
            catch (Exception ex)
            {

                throw new AccountServiceGetRoleException("Can't get role : " + ex.Message);
            }
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            Validator.ObjectIsNull(
              user, $"{nameof(AccountService)} : {nameof(IsSignedIn)} : {nameof(user)} : object is null");

            try
            {
                var result =  this.signInManager.IsSignedIn(user, isPersistent: false);

                return result;
            }
            catch (Exception ex)
            {
                throw new AccountServiceIsSignInException("Can't sign in with role: " + ex.Message);
            }
        }

        public async Task<ApplicationUser> RegisterAccountAsync(string userName, string email, string password)
        {
            Validator.StringIsNullOrEmpty(
                userName, $"{nameof(AccountService)} : {nameof(RegisterAccountAsync)} : {nameof(userName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                email, $"{nameof(AccountService)} : {nameof(RegisterAccountAsync)} : {nameof(email)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                password, $"{nameof(AccountService)} : {nameof(RegisterAccountAsync)} : {nameof(password)} : is null/empty");

            var newUser = new ApplicationUser()
            {
                UserName = userName,
                Email = email,
            };

            try
            {
                var result = await this.userManager.CreateAsync(newUser, password);


                if (result.Succeeded)
                {
                    await this.userManager.AddToRoleAsync(newUser, "Client");

                    await this.signInManager.SignInAsync(newUser, isPersistent: false);

                    return newUser;
                }
                else
                {
                    throw new AccountServiceCreateAccountException($"Account creation fails : {result.Errors}");
                }


            }
            catch (Exception ex)
            {
                throw new AccountServiceCreateAccountException("Can't register user:" + ex.Message);
            }
        }

        public async Task<ApplicationUser> RetrieveUserAsync(ClaimsPrincipal user)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(RetrieveUserAsync)} : {nameof(user)} : object is null");

            try
            {
                return await this.userManager.GetUserAsync(user);
            }
            catch (Exception ex)
            {

                throw new AccountServiceGetRoleException("Can't get role : " + ex.Message);
            }
           
        }
    }
}
