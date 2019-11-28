using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.RepoHubConnectorService.Exceptions;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepoHubConnectorService
{
    public class RepoHubKeyMaker : IRepoHubKeyMaker
    {
        private readonly IRepoUserKey repoUserKey;

        public RepoHubKeyMaker(IRepoUserKey repoUserKey)
        {
            this.repoUserKey = repoUserKey ?? throw new ArgumentNullException(nameof(repoUserKey));
        }

        public async Task<bool> CreateKey(string accesToken, string key, string title)
        {
            Validator.StringIsNullOrEmpty(
             accesToken, $"{nameof(RepoHubKeyMaker)} : {nameof(CreateKey)} : {nameof(accesToken)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             key, $"{nameof(RepoHubKeyMaker)} : {nameof(CreateKey)} : {nameof(key)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             title, $"{nameof(RepoHubKeyMaker)} : {nameof(CreateKey)} : {nameof(title)} : is null/empty");

            try
            {
                var keyMakerCall = await this.repoUserKey.AddKey(accesToken, key, title);

                return keyMakerCall;
            }
            catch (Exception ex)
            {
                throw new RepoHubKeyMakerCreateKeyException($"{nameof(RepoHubKeyMakerCreateKeyException)} : Exception : Can't prepare for hosting hub creation! : {ex.Message}");
            }
        }
    }
}