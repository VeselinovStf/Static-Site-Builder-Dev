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

        public async Task AddToRoleAsync(ApplicationUser user, string role)
        {
            Validator.ObjectIsNull(
             user, $"{nameof(AccountService)} : {nameof(AddToRoleAsync)} : {nameof(user)} : object is null");

            Validator.StringIsNullOrEmpty(
               role, $"{nameof(AccountService)} : {nameof(AddToRoleAsync)} : {nameof(role)} : is null/empty");

            try
            {
                
                await this.userManager.AddToRoleAsync(user, role);
               
            }
            catch (Exception ex)
            {

                throw new AccountServiceAddToReleException(ex.Message);
            }
        }

        public async Task<bool> ConfirmEmailAsync(ApplicationUser user, string code)
        {
            Validator.ObjectIsNull(
                user, $"{nameof(AccountService)} : {nameof(ConfirmEmailAsync)} : {nameof(user)} : object is null");

            Validator.StringIsNullOrEmpty(
             code, $"{nameof(AccountService)} : {nameof(AddToRoleAsync)} : {nameof(code)} : is null/empty");

            try
            {
                var confirmResult = await this.userManager.ConfirmEmailAsync(user, code);

                if (confirmResult.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {

                throw new AccountServiceConfirmEmailException($"Can't confirm user : {ex.Message}");
            }
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            Validator.StringIsNullOrEmpty(
                userId, $"{nameof(AccountService)} : {nameof(FindByIdAsync)} : {nameof(userId)} : is null/empty");

            try
            {
                var user = await this.userManager.FindByIdAsync(userId);

                return user;
            }
            catch (Exception ex)
            {

                throw new AccountServiceFindByIdException($"Can't find user with provided id : {ex.Message}");
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            Validator.ObjectIsNull(
                user, $"{nameof(AccountService)} : {nameof(GenerateEmailConfirmationTokenAsync)} : {nameof(user)} : object is null");

           
            try
            {
                return await this.userManager.GenerateEmailConfirmationTokenAsync(user);
            }
            catch (Exception ex)
            {
                throw new AccountServiceGenerateEmailConfirmationTokenException($"Can't create confirmation token : {ex.Message}");
            }
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

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(SignInAsync)} : {nameof(user)} : object is null");

            try
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
            }
            catch (Exception ex)
            {

                throw new AccountServiceIsSignInException(ex.Message);
            }
        }
    }
}
