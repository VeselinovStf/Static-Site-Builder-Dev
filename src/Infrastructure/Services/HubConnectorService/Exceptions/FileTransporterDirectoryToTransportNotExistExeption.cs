using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.HubConnectorService.Exceptions
{
    public class FileTransporterDirectoryToTransportNotExistExeption : Exception
    {
        public FileTransporterDirectoryToTransportNotExistExeption()
        {
        }

        public FileTransporterDirectoryToTransportNotExistExeption(string message) : base(message)
        {
        }

        public FileTransporterDirectoryToTransportNotExistExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileTransporterDirectoryToTransportNotExistExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}