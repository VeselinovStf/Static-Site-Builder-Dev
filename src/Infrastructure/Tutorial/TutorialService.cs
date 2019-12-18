using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Identity;
using Infrastructure.Tutorial.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tutorial
{
    public class TutorialService : ITutorialService
    {
        private readonly IAccountService<ApplicationUser> accountService;

        public TutorialService(
            IAccountService<ApplicationUser> accountService)
        {
            this.accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }

        public async Task<bool> ChangeTutorialStatusAsync(string clientId)
        {
            try
            {
                Validator.StringIsNullOrEmpty(
                 clientId, $"{nameof(TutorialService)} : {nameof(ChangeTutorialStatusAsync)} : {nameof(clientId)} : is null/empty");

                var client = await this.accountService.FindByIdAsync(clientId);

                Validator.ObjectIsNull(
                    client, $"{nameof(TutorialService)} : {nameof(ChangeTutorialStatusAsync)} : {nameof(client)} : Can't find client");

                return await this.accountService.ChangeTutorialAsync(client);
            }
            catch (Exception ex)
            {

                throw new TutorialServiceGIsClientInTutorialException($"{nameof(TutorialServiceGIsClientInTutorialException)} : Exception : Can't return clint tutorial status : {ex.Message}");

            }
        }

        public async Task<bool> IsClientInTutorial(string clientId)
        {
            try
            {
                Validator.StringIsNullOrEmpty(
                 clientId, $"{nameof(TutorialService)} : {nameof(IsClientInTutorial)} : {nameof(clientId)} : is null/empty");

                var client = await this.accountService.FindByIdAsync(clientId);


                Validator.ObjectIsNull(
                    client, $"{nameof(TutorialService)} : {nameof(IsClientInTutorial)} : {nameof(client)} : Can't find client");

                return client.IsInTutorial;
            }
            catch (Exception ex)
            {

                throw new TutorialServiceGIsClientInTutorialException($"{nameof(TutorialServiceGIsClientInTutorialException)} : Exception : Can't return clint tutorial status : {ex.Message}");

            }
        }
    }
}
