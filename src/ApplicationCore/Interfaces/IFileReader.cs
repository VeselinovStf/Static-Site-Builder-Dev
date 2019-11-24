using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string file);
    }
}