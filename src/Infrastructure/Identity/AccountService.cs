using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Identity.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
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
                throw new AccountServiceAddToReleException($"{nameof(AccountServiceAddToReleException)} : Can't add to role{ex.Message}");
            }
        }

        //TODO: Add Custom Db Search or validation
        public async Task<bool> ConfirmCangePasswordAsync(string userId, string code)
        {
            Validator.StringIsNullOrEmpty(
            userId, $"{nameof(AccountService)} : {nameof(ConfirmCangePasswordAsync)} : {nameof(userId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             code, $"{nameof(AccountService)} : {nameof(ConfirmCangePasswordAsync)} : {nameof(code)} : is null/empty");

            try
            {
                var user = await this.userManager.FindByIdAsync(userId);

                return true;
            }
            catch (Exception ex)
            {
                throw new AccountServiceConfirmCangePasswordException($"{nameof(AccountServiceConfirmCangePasswordException)}: Can't confirm user password : {ex.Message}");
            }
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string code)
        {
            Validator.StringIsNullOrEmpty(
             userId, $"{nameof(AccountService)} : {nameof(ConfirmEmailAsync)} : {nameof(userId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             code, $"{nameof(AccountService)} : {nameof(ConfirmEmailAsync)} : {nameof(code)} : is null/empty");

            try
            {
                var user = await this.userManager.FindByIdAsync(userId);

                var confirmResult = await this.userManager.ConfirmEmailAsync(user, code);

                if (confirmResult.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new AccountServiceConfirmEmailException($"{nameof(AccountServiceConfirmEmailException)}: Can't confirm user : {ex.Message}");
            }
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            Validator.StringIsNullOrEmpty(
                email, $"{nameof(AccountService)} : {nameof(FindByEmailAsync)} : {nameof(email)} : is null/empty");

            try
            {
                var user = await this.userManager.FindByEmailAsync(email);

                return user;
            }
            catch (Exception ex)
            {
                throw new AccountServiceFindByEmailException($"{nameof(AccountServiceFindByEmailException)}Can't find user with provided email : {ex.Message}");
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
                throw new AccountServiceFindByIdException($"{nameof(AccountServiceFindByIdException)}Can't find user with provided id : {ex.Message}");
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
                throw new AccountServiceGenerateEmailConfirmationTokenException($"{nameof(AccountServiceGenerateEmailConfirmationTokenException)}: Can't create confirmation token : {ex.Message}");
            }
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(GeneratePasswordResetTokenAsync)} : {nameof(user)} : object is null");

            try
            {
                return await this.userManager.GeneratePasswordResetTokenAsync(user);
            }
            catch (Exception ex)
            {
                throw new AccountServiceGeneratePasswordConfirmationTokenException($"{nameof(AccountServiceGeneratePasswordConfirmationTokenException)}: Can't create confirmation token : {ex.Message}");
            }
        }

        public async Task<bool> UpdateUserName(ApplicationUser user, string newUserName)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(UpdateUserName)} : {nameof(user)} : object is null");

            Validator.StringIsNullOrEmpty(
                newUserName, $"{nameof(AccountService)} : {nameof(UpdateUserName)} : {nameof(newUserName)} : is null/empty");

            if (user.UserName.Equals(newUserName))
            {
                return true;
            }

            try
            {
                var callResult = await this.userManager.ChangeUserNameAsync(user, newUserName);

                if (callResult.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new AccountServiceUpdateUserNameException($"{nameof(AccountServiceUpdateUserNameException)}: Can't Update user name : {ex.Message}");
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
                throw new AccountServiceGetRoleException($"{nameof(AccountServiceGetRoleException)}: Can't get role : " + ex.Message);
            }
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            Validator.ObjectIsNull(
              user, $"{nameof(AccountService)} : {nameof(IsSignedIn)} : {nameof(user)} : object is null");

            try
            {
                var result = this.signInManager.IsSignedIn(user, isPersistent: false);

                return result;
            }
            catch (Exception ex)
            {
                throw new AccountServiceIsSignInException($"{nameof(AccountServiceIsSignInException)}: Can't sign in with role: " + ex.Message);
            }
        }

        public async Task PasswordSignInAsync(string email, string password, bool rememberMe, bool lockoutOnFailure)
        {
            Validator.StringIsNullOrEmpty(
               email, $"{nameof(AccountService)} : {nameof(PasswordSignInAsync)} : {nameof(email)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              password, $"{nameof(AccountService)} : {nameof(PasswordSignInAsync)} : {nameof(password)} : is null/empty");

            var user = await this.userManager.FindByEmailAsync(email);

            Validator.ObjectIsNull(
                user, $"{nameof(AccountService)} : {nameof(PasswordSignInAsync)} : {nameof(user)} : object is null");

            if (user.EmailConfirmed && !user.IsDeleted)
            {
                var result = await this.signInManager.PasswordSignInAsync(user.UserName, password, rememberMe, lockoutOnFailure);

                if (!result.Succeeded)
                {
                    throw new AccountServiceAccountInvalidLoginAttempt($"{nameof(AccountServiceAccountInvalidLoginAttempt)}: Invalid login attempt");
                }

                if (result.IsLockedOut)
                {
                    throw new AccountServiceAccountLockedOutException($"{nameof(AccountServiceAccountLockedOutException)}: User account locked out.");
                }
            }
            else
            {
                throw new AccountServiceAccountEmailNotConfirmedException($"{nameof(AccountServiceAccountEmailNotConfirmedException)}: User Email is not Confirmed");
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

            var result = await this.userManager.CreateAsync(newUser, password);

            if (result.Succeeded)
            {
                return newUser;
            }
            else
            {
                throw new AccountServiceCreateAccountException($"{nameof(AccountServiceCreateAccountException)}:Account creation fails : {result.Errors}");
            }
        }

        public async Task<bool> ResetPasswordAsync(string userName, string password, string confirmPassword, string token)
        {
            Validator.StringIsNullOrEmpty(
             userName, $"{nameof(AccountService)} : {nameof(ResetPasswordAsync)} : {nameof(userName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                password, $"{nameof(AccountService)} : {nameof(ResetPasswordAsync)} : {nameof(password)} : is null/empty");

            Validator.StringIsNullOrEmpty(
                confirmPassword, $"{nameof(AccountService)} : {nameof(ResetPasswordAsync)} : {nameof(confirmPassword)} : is null/empty");

            Validator.StringEqualsString(
            password, confirmPassword, $"{nameof(AccountService)} : {nameof(ResetPasswordAsync)} : {nameof(password)} : two strings are not equal");

            Validator.StringIsNullOrEmpty(
             token, $"{nameof(AccountService)} : {nameof(ResetPasswordAsync)} : {nameof(token)} : is null/empty");

            try
            {
                var user = await this.userManager.FindByNameAsync(userName);

                var confirmResult = await this.userManager.ResetPasswordAsync(user, token, password);

                if (confirmResult.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new AccountServiceResetPasswordException($"{nameof(AccountServiceResetPasswordException)}: Can't reset user password : {ex.Message}");
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
                throw new AccountServiceRetrieveUserException($"{nameof(AccountServiceRetrieveUserException)}: Can't get role : {ex.Message}");
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
                throw new AccountServiceIsSignInException($"{nameof(AccountServiceIsSignInException)} : Can't Sing In with this user : {ex.Message}");
            }
        }

        public async Task SignOutAsync()
        {
            await this.signInManager.SignOutAsync();
        }

        public async Task<bool> DeleteUser(ApplicationUser user)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(DeleteUser)} : {nameof(user)} : object is null");

            try
            {
                var deleteCall = await this.userManager.DeleteClient(user.Id);

                if (deleteCall.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new AccountServiceDeleteUserException($"{nameof(AccountServiceDeleteUserException)} : Can't Delete this user : {ex.Message}");
            }
        }

        public async Task<ApplicationUser> GetPaymentsAsync(ApplicationUser user)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(DeleteUser)} : {nameof(user)} : object is null");

            try
            {
                //TODO: IMPLEMENT THE PAYMENT IN NEXT RUN
                return await this.userManager.FindByIdAsync(user.Id);
            }
            catch (Exception ex)
            {
                throw new AccountServiceGetPaymentsException($"{nameof(AccountServiceGetPaymentsException)} : Can't Get User Payments : {ex.Message}");
            }
        }

        public async Task<bool> ChangeEmailAsync(ApplicationUser user, string newEmail, string emailUpdateConfirmationCode)
        {
            Validator.ObjectIsNull(
               user, $"{nameof(AccountService)} : {nameof(ChangeEmailAsync)} : {nameof(user)} : object is null");

            Validator.StringIsNullOrEmpty(
               newEmail, $"{nameof(AccountService)} : {nameof(ChangeEmailAsync)} : {nameof(newEmail)} : is null/empty");

            Validator.StringIsNullOrEmpty(
               emailUpdateConfirmationCode, $"{nameof(AccountService)} : {nameof(ChangeEmailAsync)} : {nameof(emailUpdateConfirmationCode)} : is null/empty");

            try
            {
                var result = await this.userManager.ChangeEmailNameAsync(user, newEmail, emailUpdateConfirmationCode);

                if (result.Succeeded)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new AccountServiceGetPaymentsException($"{nameof(AccountServiceGetPaymentsException)} : Can't Change User Email : {ex.Message}");
            }
        }
    }
}