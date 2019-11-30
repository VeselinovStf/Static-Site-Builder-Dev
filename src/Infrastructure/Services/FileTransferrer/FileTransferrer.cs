using ApplicationCore.Interfaces;
using Infrastructure.ExtensionMethods;
using Infrastructure.Services.FileTransferrer.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services.FileTransferrer
{
    public class FileTransferrer : IFileTransferrer<ConvertedFileElement>
    {
        private readonly IFileReader fileReader;
        private readonly IList<string> imageExtensions;

        public FileTransferrer(
            IFileReader fileReader,
            IList<string> imageExtensions)
        {
            this.fileReader = fileReader ?? throw new ArgumentNullException(nameof(fileReader));
            this.imageExtensions = imageExtensions ?? throw new ArgumentNullException(nameof(imageExtensions));
        }

        public async Task<IList<ConvertedFileElement>> FilesToList(string sourceDirName, bool copySubDirs = true)
        {
            var filePaths = new List<string>();
            var fileContents = new List<string>();

            await this.DirectoryCoppy(sourceDirName, filePaths, fileContents);

            var resultElement = new List<ConvertedFileElement>(filePaths.Zip(fileContents, (fp, fc) => new ConvertedFileElement()
            {
                FilePath = fp,
                FileContent = fc
            }));

            return resultElement;
        }

        /// <summary>
        /// Recursive method for iterrating over directory, iterrates all files and
        /// all sub directorys ( if copy sub dirs is on ). All results are passed
        /// to filePaths and fileContent by refference
        /// </summary>
        /// <param name="sourceDirName">Source of files</param>
        /// <param name="filePaths">Generated file paths list</param>
        /// <param name="fileContents">Generated Content of each file</param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        /// <returns>filePaths and fileContent reference results</returns>
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

                    if (this.imageExtensions.Any(e => temppath.Contains(e)))
                    {
                        fileContents.Add(file.FullName.GetBase64String());
                    }
                    else
                    {
                        fileContents.Add(fileContent);
                    }
                }
                else
                {
                    string temppath = destDirName + "/" + file.Name;

                    filePaths.Add(temppath);

                    var fileContent = await this.fileReader.ReadFileAsync(file.FullName);

                    if (this.imageExtensions.Any(e => temppath.Contains(e)))
                    {
                        fileContents.Add(file.FullName.GetBase64String());
                    }
                    else
                    {
                        fileContents.Add(fileContent);
                    }
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

        private string ImageFormatter(string imagePath)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(imagePath);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            return base64ImageRepresentation;
        }
    }
}