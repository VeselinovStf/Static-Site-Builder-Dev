using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ISiteStorageCreatorService
    {
        Task<bool> StorageCreatorExecute();
    }
}