using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFileTransferrer<T>
    {
        /// <summary>
        /// Gets souce dir name and return list of T, where T is object containing file path and content
        /// </summary>
        /// <param name="sourceDirName">Source of Directory name</param>
        /// <param name="copySubDirs">Poppy ir not sub dirrectoryes</param>
        /// <returns>List of T - object containing path and content of each file as string</returns>
        Task<IList<T>> FilesToList(string sourceDirName, bool copySubDirs = true);
    }
}