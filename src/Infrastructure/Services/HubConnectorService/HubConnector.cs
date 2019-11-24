using ApplicationCore.Interfaces;
using Infrastructure.Guard;
using Infrastructure.Services.APIClientService.Clients;
using Infrastructure.Services.HubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services.HubConnectorService
{
    public class HubConnector : IHubConnector
    {
        private readonly IAPIClientService<GitLabHubClient> clientHub;
        private readonly IFileReader fileReader;

        public HubConnector(
            IOptions<AuthHubConnectorOptions> options,
            IAPIClientService<GitLabHubClient> clientHub,
            IFileReader fileReader)
        {
            this.Options = options.Value;
            this.clientHub = clientHub ?? throw new ArgumentNullException(nameof(clientHub));
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
        }

        public AuthHubConnectorOptions Options { get; }

        public async Task<string> CreateHub(string name)
        {
            return await ExecuteCreate(name, Options.AccesTokken);
        }

        private async Task<string> ExecuteCreate(string name, string accesTokken)
        {
            Validator.StringIsNullOrEmpty(
              name, $"{nameof(HubConnector)} : {nameof(ExecuteCreate)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesTokken, $"{nameof(HubConnector)} : {nameof(ExecuteCreate)} : {nameof(accesTokken)} : is null/empty");

            try
            {
                var clientHubCallId = await this.clientHub.CreateHubAsync(name, accesTokken);

                Validator.StringIsNullOrEmpty(
                        clientHubCallId, $"{nameof(HubConnector)} : {nameof(ExecuteCreate)} : {nameof(clientHubCallId)} : is null/empty");

                return clientHubCallId;
            }
            catch (Exception ex)
            {
                throw new HubConnectorCreateHubException($"{nameof(HubConnectorCreateHubException)} : Exception : Can't create hub! : {ex.Message}");
            }
        }

        public async Task<bool> PushProject(string hubId, string sourceDirName, bool copySubDir = true)
        {
            return await ExecutePush(hubId, sourceDirName, Options.AccesTokken, copySubDir);
        }

        private async Task<bool> ExecutePush(string hubId, string sourceDirName, string accesTokken, bool copySubDirs = true, string destDirName = "")
        {
            Validator.StringIsNullOrEmpty(
              hubId, $"{nameof(HubConnector)} : {nameof(ExecutePush)} : {nameof(hubId)} : is null/empty");

            Validator.StringIsNullOrEmpty(
             sourceDirName, $"{nameof(HubConnector)} : {nameof(ExecutePush)} : {nameof(sourceDirName)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesTokken, $"{nameof(HubConnector)} : {nameof(ExecutePush)} : {nameof(accesTokken)} : is null/empty");

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
                throw new HubConnectorExecutePushException($"{nameof(HubConnectorExecutePushException)} : Can't Execute Push to Hub : {ex.Message}");
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

        public void WholeDirectoryTransport(string newLocation, string dirToTransportLocation)
        {
            if (Directory.Exists(dirToTransportLocation))
            {
                if (Directory.Exists(newLocation))
                {
                    throw new FileTransporterNewLocationExistsExeption($"{nameof(FileTransporterNewLocationExistsExeption)} : New Location Folder exists - {newLocation}");
                }
                else
                {
                    try
                    {
                        Directory.Move(dirToTransportLocation, newLocation);
                    }
                    catch (Exception ex)
                    {
                        throw new FileTransporterContentNotExistException($"{nameof(FileTransporterContentNotExistException)} : FATAL ERROR : params - {newLocation}, {dirToTransportLocation} : {ex.Message}");
                    }
                }
            }
            else
            {
                throw new FileTransporterDirectoryToTransportNotExistExeption($"{nameof(FileTransporterDirectoryToTransportNotExistExeption)} : No Files Directory to transport From");
            }
        }
    }

    public class FileContent
    {
        public string FilePath { get; set; }

        public string Content { get; set; }
    }
}