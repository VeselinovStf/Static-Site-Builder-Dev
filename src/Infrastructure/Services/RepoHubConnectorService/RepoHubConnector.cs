using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.RepoHubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepoHubConnectorService
{
    public class RepoHubConnector : IRepoHubConnector
    {
        private readonly IAPIRepoClientService<GitLabHubClient> clientHub;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplatesService;
        private readonly IFileReader fileReader;

        public RepoHubConnector(
            IOptions<AuthRepoHubConnectorOptions> repoOptions,

            IAPIRepoClientService<GitLabHubClient> clientHub,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplatesService,
            IFileReader fileReader)
        {
            this.RepoOptions = repoOptions.Value;
            this.clientHub = clientHub ?? throw new ArgumentNullException(nameof(clientHub));
            this.appSiteTemplatesService = appSiteTemplatesService ?? throw new ArgumentNullException(nameof(appSiteTemplatesService));
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public AuthRepoHubConnectorOptions RepoOptions { get; }

        public async Task<string> CreateHub(string name, string accesToken)
        {
            Validator.StringIsNullOrEmpty(
              name, $"{nameof(RepoHubConnector)} : {nameof(CreateHub)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesToken, $"{nameof(RepoHubConnector)} : {nameof(CreateHub)} : {nameof(accesToken)} : is null/empty");

            try
            {
                var clientHubCallId = await this.clientHub.CreateHubAsync(name, accesToken);

                Validator.StringIsNullOrEmpty(
                        clientHubCallId, $"{nameof(RepoHubConnector)} : {nameof(CreateHub)} : {nameof(clientHubCallId)} : is null/empty");

                return clientHubCallId;
            }
            catch (Exception ex)
            {
                throw new RepoHubConnectorCreateHubException($"{nameof(RepoHubConnectorCreateHubException)} : Exception : Can't create repo hub! : {ex.Message}");
            }
        }

        public async Task<bool> PushProject(string hubId, string templateName, bool copySubDir = true)
        {
            return await ExecutePush(hubId, templateName, RepoOptions.AccesTokken, copySubDir);
        }

        private async Task<bool> ExecutePush(string hubId, string templateName, string accesTokken, bool copySubDirs = true, string destDirName = "")
        {
            Validator.StringIsNullOrEmpty(
              hubId, $"{nameof(RepoHubConnector)} : {nameof(ExecutePush)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             templateName, $"{nameof(RepoHubConnector)} : {nameof(ExecutePush)} : {nameof(templateName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesTokken, $"{nameof(RepoHubConnector)} : {nameof(ExecutePush)} : {nameof(accesTokken)} : is null/empty");

            try
            {
                var filePaths = new List<string>();
                var fileContents = new List<string>();

                var templateWithElements = await this.appSiteTemplatesService.GetTemplateWithElementsAsync(templateName);

                filePaths = new List<string>(templateWithElements.SiteTemplateElements.Select(p => p.FilePath));
                fileContents = new List<string>(templateWithElements.SiteTemplateElements.Select(p => p.FileContent));

                var clientHubResult = await this.clientHub.PushDataToHub(hubId, accesTokken, filePaths, fileContents);

                if (clientHubResult)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new RepoHubConnectorExecutePushException($"{nameof(RepoHubConnectorExecutePushException)} : Can't Execute Push to Hub : {ex.Message}");
            }
        }
    }
}