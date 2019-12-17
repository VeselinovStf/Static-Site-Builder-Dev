using ApplicationCore.Entities.SitesTemplates;
using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.APIClientService.DTOs;
using Infrastructure.Services.RepoHubConnectorService.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepoHubConnectorService
{
    public class RepoHubConnector : IRepoHubConnector<RepoPullTemplateDTO>
    {
        private readonly IAPIRepoClientService<GitLabHubClient> clientHub;
        private readonly IAppSiteTemplatesService<SiteTemplate> appSiteTemplatesService;
        private readonly IAPIRepoClientService<RepoPullTemplateDTO> apiRepoClientService;

        public RepoHubConnector(
            IAPIRepoClientService<GitLabHubClient> clientHub,
            IAppSiteTemplatesService<SiteTemplate> appSiteTemplatesService,
            IAPIRepoClientService<RepoPullTemplateDTO> apiRepoClientService
            )
        {
            this.clientHub = clientHub ?? throw new ArgumentNullException(nameof(clientHub));
            this.appSiteTemplatesService = appSiteTemplatesService ?? throw new ArgumentNullException(nameof(appSiteTemplatesService));
            this.apiRepoClientService = apiRepoClientService ?? throw new ArgumentNullException(nameof(apiRepoClientService));
        }

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

        public async Task<RepoPullTemplateDTO> PullDataFromHub(string hubId, string repositoryName, string accesToken)
        {
            Validator.StringIsNullOrEmpty(
              hubId, $"{nameof(RepoHubConnector)} : {nameof(PullDataFromHub)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             repositoryName, $"{nameof(RepoHubConnector)} : {nameof(PullDataFromHub)} : {nameof(repositoryName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesToken, $"{nameof(RepoHubConnector)} : {nameof(PullDataFromHub)} : {nameof(accesToken)} : is null/empty");

            try
            {
                var result = await this.apiRepoClientService.PullDataFromHub(hubId, repositoryName, accesToken);

                return result;
            }
            catch (Exception ex)
            {

                throw new RepoHubConnectorPullDataFromHubException($"{nameof(RepoHubConnectorPullDataFromHubException)} : Can't Execute Pull to Hub : {ex.Message}");

            }
        }

        public async Task<bool> PushProject(string hubId, string templateName, string accesToken, bool copySubDir = true)
        {
            Validator.StringIsNullOrEmpty(
              hubId, $"{nameof(RepoHubConnector)} : {nameof(PushProject)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             templateName, $"{nameof(RepoHubConnector)} : {nameof(PushProject)} : {nameof(templateName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesToken, $"{nameof(RepoHubConnector)} : {nameof(PushProject)} : {nameof(accesToken)} : is null/empty");

            try
            {
                var filePaths = new List<string>();
                var fileContents = new List<string>();

                var templateWithElements = await this.appSiteTemplatesService.GetTemplateWithElementsAsync(templateName);

                filePaths = new List<string>(templateWithElements.SiteTemplateElements.Select(p => p.FilePath));
                fileContents = new List<string>(templateWithElements.SiteTemplateElements.Select(p => p.FileContent));

                var clientHubResult = await this.clientHub.PushDataToHub(hubId, accesToken, filePaths, fileContents);

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