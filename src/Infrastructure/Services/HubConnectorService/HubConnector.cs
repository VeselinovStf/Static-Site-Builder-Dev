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

        public HubConnector(
            IOptions<AuthHubConnectorOptions> options,
            IAPIClientService<GitLabHubClient> clientHub)
        {
            this.Options = options.Value;
            this.clientHub = clientHub ?? throw new ArgumentNullException(nameof(clientHub));
        }

        public AuthHubConnectorOptions Options { get; }

        public async Task<bool> CreateHub(string name)
        {
            return await ExecuteCreate(name, Options.AccesTokken);
        }

        private async Task<bool> ExecuteCreate(string name, string accesTokken)
        {
            Validator.StringIsNullOrEmpty(
              name, $"{nameof(HubConnector)} : {nameof(ExecuteCreate)} : {nameof(name)} : is null/empty");

            Validator.StringIsNullOrEmpty(
              accesTokken, $"{nameof(HubConnector)} : {nameof(ExecuteCreate)} : {nameof(accesTokken)} : is null/empty");

            try
            {
                var clientHubCall = await this.clientHub.CreateHubAsync(name, accesTokken);

                if (clientHubCall)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new HubConnectorCreateHubException($"{nameof(HubConnectorCreateHubException)} : Exception : Can't create hub! : {ex.Message}");
            }
        }

        public async Task<bool> PushProject(string hubName, string sourceDirName, bool copySubDir = true)
        {
            return await ExecutePush(hubName, sourceDirName, Options.AccesTokken, copySubDir);
        }

        private async Task<bool> ExecutePush(string hubName, string sourceDirName, string accesTokken, bool copySubDirs = true, string destDirName = "")
        {
            var branch = "master";
            var commitMessage = "Initial";
            var actions = "create";

            //var filePath = new List<string>();
            //var contents = new List<string>();

            //USE TWO LISTS TO PASS DATA, if the end function is pre-created to add more to the hub action
            var directoryCoppy = new List<FileContent>();

            DirectoryCoppy(sourceDirName, directoryCoppy);

            return false;
        }

        private void DirectoryCoppy(string sourceDirName, List<FileContent> content, string destDirName = "", bool copySubDirs = true)
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

                    content.Add(new FileContent()
                    {
                        FilePath = temppath,
                        Content = temppath + " 1"
                    });
                }
                else
                {
                    string temppath = destDirName + "/" + file.Name;

                    content.Add(new FileContent()
                    {
                        FilePath = temppath,
                        Content = temppath + " 1"
                    });
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

                        DirectoryCoppy(subdir.FullName, content, temppath, copySubDirs);
                    }
                    else
                    {
                        string temppath = destDirName + "/" + subdir.Name;

                        DirectoryCoppy(subdir.FullName, content, temppath, copySubDirs);
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