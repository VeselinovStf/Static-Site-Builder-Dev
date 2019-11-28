using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteStorageCreatorService
    {
        Task<bool> StorageCreatorExecute(string newRepositoryCreateName, string templateName);
    }
}