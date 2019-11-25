using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHubConnector
    {
        /// <summary>
        /// Create site project hub
        /// </summary>
        /// <param name="name">Name of the project, used for hub name</param>
        /// <returns>Id of created hub</returns>
        Task<string> CreateHub(string name);

        /// <summary>
        /// Uploading new project directly to hub
        /// </summary>
        /// <param name="hubProjectName">Project name created by CreateHub</param>
        /// <param name="sourceDirName">Source of ptoject template</param>
        /// <param name="copySubDir">copy all or coppy only file</param>
        /// <returns>Pushed or not bool value</returns>
        Task<bool> PushProject(string hubProjectId, string sourceDirName, bool copySubDir = true);

        /// <summary>
        /// Coppy whole directory from one place to other
        /// Main idea is to move template project from one place to other.
        /// Key word "Dirrectory" may be used as repository
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="destDisName">Destination directory</param>
        /// <param name="copySubDirs">Default set to coppy all sub directoies</param>
        // void DirectoryCoppy(string sourceDirName, string destDisName, bool copySubDirs = true);
    }
}