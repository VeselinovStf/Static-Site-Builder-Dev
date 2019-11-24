using ApplicationCore.Interfaces;
using Infrastructure.Services.FileReader.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services.FileReader
{
    public class FileReader : IFileReader
    {
        public async Task<string> ReadFileAsync(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line = await sr.ReadToEndAsync();

                    return line;
                }
            }
            catch (Exception ex)
            {
                throw new FileReaderException($"{nameof(FileReaderException)} : Can't read file : {ex.Message}");
            }
        }
    }
}