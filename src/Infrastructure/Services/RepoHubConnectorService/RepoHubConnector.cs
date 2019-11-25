using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.HostingHubConnectorService;
using Infrastructure.Services.RepoHubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services.RepoHubConnectorService
{
    public class RepoHubConnector : IHubConnector, IHubConnectorRepoOption
    {
        private readonly IAPIRepoClientService<GitLabHubClient> clientHub;
        private readonly IFileReader fileReader;

        public RepoHubConnector(
            IOptions<AuthRepoHubConnectorOptions> repoOptions,
            IOptions<AuthHostingConnectorOptions> hostingOptions,
            IAPIRepoClientService<GitLabHubClient> clientHub,
            IFileReader fileReader)
        {
            this.HostingOptions = hostingOptions.Value;
            this.RepoOptions = repoOptions.Value;
            this.clientHub = clientHub ?? throw new ArgumentNullException(nameof(clientHub));
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

        public async Task<bool> PushProject(string hubId, string sourceDirName, bool copySubDir = true)
        {
            return await ExecutePush(hubId, sourceDirName, RepoOptions.AccesTokken, copySubDir);
        }

        private async Task<bool> ExecutePush(string hubId, string sourceDirName, string accesTokken, bool copySubDirs = true, string destDirName = "")
        {
            Validator.StringIsNullOrEmpty(
              hubId, $"{nameof(RepoHubConnector)} : {nameof(ExecutePush)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             sourceDirName, $"{nameof(RepoHubConnector)} : {nameof(ExecutePush)} : {nameof(sourceDirName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesTokken, $"{nameof(RepoHubConnector)} : {nameof(ExecutePush)} : {nameof(accesTokken)} : is null/empty");

            try
            {
                var filePaths = new List<string>();
                var fileContents = new List<string>();

                await DirectoryCoppy(sourceDirName, filePaths, fileContents);

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

        private async Task DirectoryCoppy(string sourceDirName, List<string> filePaths, List<string> fileContents, string destDirName = "", bool copySubDirs = true)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();

            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (destDirName == "")
                {
                    string temppath = destDirName + file.Name;

                    filePaths.Add(temppath);

                    var fileContent = await this.fileReader.ReadFileAsync(file.FullName);

                    fileContents.Add(fileContent);
                }
                else
                {
                    string temppath = destDirName + "/" + file.Name;

                    filePaths.Add(temppath);

                    var fileContent = await this.fileReader.ReadFileAsync(file.FullName);

                    fileContents.Add(fileContent);
                }
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    if (destDirName == "")
                    {
                        string temppath = destDirName + subdir.Name;

                        await DirectoryCoppy(subdir.FullName, filePaths, fileContents, temppath, copySubDirs);
                    }
                    else
                    {
                        string temppath = destDirName + "/" + subdir.Name;

                        await DirectoryCoppy(subdir.FullName, filePaths, fileContents, temppath, copySubDirs);
                    }
                }
            }
        }

        public async Task<bool> AddCiCDVariables(string hubId, string hostingId)
        {
            return await this.AddVariables(hubId, HostingOptions.HostAccesToken, hostingId);
        }

        private async Task<bool> AddVariables(string hubId, string hostingAccesToken, string hostingId)
        {
            Validator.StringIsNullOrEmpty(
             hubId, $"{nameof(RepoHubConnector)} : {nameof(AddCiCDVariables)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              hostingId, $"{nameof(RepoHubConnector)} : {nameof(AddCiCDVariables)} : {nameof(hostingId)} : is null/empty");

            try
            {
                var clientHubCall = await this.clientHub.AddVariables(hubId, hostingAccesToken, hostingId);

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