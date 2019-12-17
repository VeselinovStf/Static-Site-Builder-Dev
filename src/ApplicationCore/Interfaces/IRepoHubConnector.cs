using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IRepoHubConnector<T>
    {
        /// <summary>
        /// Create site project hub
        /// </summary>
        /// <param name="name">Name of the project, used for hub name</param>
        /// <returns>Id of created hub</returns>
        Task<string> CreateHub(string name, string accesToken);

        /// <summary>
        /// Uploading new project directly to hub
        /// </summary>
        /// <param name="hubProjectName">Project name created by CreateHub</param>
        /// <param name="templateName">Name of project template template</param>
        /// <param name="copySubDir">copy all or coppy only file</param>
        /// <returns>Pushed or not bool value</returns>
        Task<bool> PushProject(string hubProjectId, string templateName, string accesToken, bool copySubDir = true);

        Task<T> PullDataFromHub(string hubId, string repositoryName, string accesTokken);
    }
}