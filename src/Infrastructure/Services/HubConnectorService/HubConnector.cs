using ApplicationCore.Interfaces;
using Infrastructure.Services.HubConnectorService.Exceptions;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services.HubConnectorService
{
    public class HubConnector : IHubConnector
    {
        public HubConnector(IOptions<AuthHubConnectorOptions> options)
        {
            this.Options = options.Value;
        }

        public AuthHubConnectorOptions Options { get; }

        public async Task<bool> CreateHub(string name)
        {
            return await ExecuteCreate(name, Options.AccesTokken);
        }

        private async Task<bool> ExecuteCreate(string name, string accesTokken)
        {
            //POST
            //CREATE PROJECT
            // https://gitlab.com/api/v4/projects?access_token=xz5NLCcyjkovPguXWzGC
            //{
            //	"name" : "Test2"
            //}
            throw new NotImplementedException();
        }

        public async Task<bool> PushProject(string hubName, string sourceDirName, bool copySubDir = true)
        {
            return await ExecutePush(hubName, sourceDirName, Options.AccesTokken, copySubDir);
        }

        private async Task<bool> ExecutePush(string hubName, string sourceDirName, string accesTokken, bool copySubDir)
        {
            throw new NotImplementedException();
        }

        public void DirectoryCoppy(string sourceDirName, string destDirName, bool copySubDirs = true)
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
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCoppy(subdir.FullName, temppath, copySubDirs);
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
}