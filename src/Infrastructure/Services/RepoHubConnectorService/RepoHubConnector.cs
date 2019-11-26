using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.HostingHubConnectorService;
using Infrastructure.Services.RepoHubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepoHubConnectorService
{
    public class RepoHubConnector : IRepoHubConnector, IHubConnectorRepoOption
    {
        private readonly IAPIRepoClientService<GitLabHubClient> clientHub;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplatesService;
        private readonly IFileReader fileReader;

        public RepoHubConnector(
            IOptions<AuthRepoHubConnectorOptions> repoOptions,
            IOptions<AuthHostingConnectorOptions> hostingOptions,
            IAPIRepoClientService<GitLabHubClient> clientHub,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplatesService,
            IFileReader fileReader)
        {
            this.HostingOptions = hostingOptions.Value;
            this.RepoOptions = repoOptions.Value;
            this.clientHub = clientHub ?? throw new ArgumentNullException(nameof(clientHub));
            this.appSiteTemplatesService = appSiteTemplatesService ?? throw new ArgumentNullException(nameof(appSiteTemplatesService));
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public AuthRepoHubConnectorOptions RepoOptions { get; }
        public AuthHostingConnectorOptions HostingOptions { get; }

        public async Task<string> CreateHub(string name)
        {
            return await ExecuteCreate(name, RepoOptions.AccesTokken);
        }

        private async Task<string> ExecuteCreate(string name, string accesTokken)
        {
            Validator.StringIsNullOrEmpty(
              name, $"{nameof(RepoHubConnector)} : {nameof(ExecuteCreate)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesTokken, $"{nameof(RepoHubConnector)} : {nameof(ExecuteCreate)} : {nameof(accesTokken)} : is null/empty");

            try
            {
                var clientHubCallId = await this.clientHub.CreateHubAsync(name, accesTokken);

                Validator.StringIsNullOrEmpty(
                        clientHubCallId, $"{nameof(RepoHubConnector)} : {nameof(ExecuteCreate)} : {nameof(clientHubCallId)} : is null/empty");

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

        public async Task<bool> AddCiCDVariables(string hubId, string hostingId)
        {
            return await this.AddVariables(hubId, RepoOptions.AccesTokken, hostingId, HostingOptions.HostAccesToken);
        }

        private async Task<bool> AddVariables(string hubId, string repoAccesToken, string hostingId, string hostingAccesToken)
        {
            Validator.StringIsNullOrEmpty(
             hubId, $"{nameof(RepoHubConnector)} : {nameof(AddCiCDVariables)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              hostingId, $"{nameof(RepoHubConnector)} : {nameof(AddCiCDVariables)} : {nameof(hostingId)} : is null/empty");

            try
            {
                var clientHubCall = await this.clientHub.AddVariables(hubId, repoAccesToken, hostingId, hostingAccesToken);

                if (clientHubCall)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new RepoHubConnectorAddCiCDVariablesException($"{nameof(RepoHubConnectorAddCiCDVariablesException)} : Exception : Can't add valiebles to CI/CD repo hub! : {ex.Message}");
            }
        }
    }
}