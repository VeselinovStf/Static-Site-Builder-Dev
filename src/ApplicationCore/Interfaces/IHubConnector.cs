using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IHubConnector
    {
        /// <summary>
        /// Create site project hub
        /// </summary>
        /// <param name="name">Name of the project, used for hub name</param>
        /// <returns>Created or not bool value</returns>
        Task<bool> CreateHub(string name);

        /// <summary>
        /// Uploading new project directly to hub
        /// </summary>
        /// <param name="hubProjectName">Project name created by CreateHub</param>
        /// <param name="sourceDirName">Source of ptoject template</param>
        /// <param name="copySubDir">copy all or coppy only file</param>
        /// <returns>Pushed or not bool value</returns>
        Task<bool> PushProject(string hubProjectName, string sourceDirName, bool copySubDir = true);

        /// <summary>
        /// Coppy whole directory from one place to other
        /// Main idea is to move template project from one place to other.
        /// Key word "Dirrectory" may be used as repository
        /// </summary>
        /// <param name="sourceDirName">Source directory</param>
        /// <param name="destDisName">Destination directory</param>
        /// <param name="copySubDirs">Default set to coppy all sub directoies</param>
        void DirectoryCoppy(string sourceDirName, string destDisName, bool copySubDirs = true);

        /// <summary>
        /// Moves Whole Directory from one place to new place.
        /// Used for remove project or to change owner by move.
        /// Key word "Dirrectory" may be used as repository
        /// </summary>
        /// <param name="newLocation">New Location</param>
        /// <param name="dirToTransportLocation">New Location</param>
        void WholeDirectoryTransport(string newLocation, string dirToTransportLocation);
    }
}