using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.FileReader.Exceptions
{
    public class FileReaderException : Exception
    {
        public FileReaderException()
        {
        }

        public FileReaderException(string message) : base(message)
        {
        }

        public FileReaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}