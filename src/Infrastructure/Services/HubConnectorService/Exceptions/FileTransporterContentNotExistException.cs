using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.HubConnectorService.Exceptions
{
    public class FileTransporterContentNotExistException : Exception
    {
        public FileTransporterContentNotExistException()
        {
        }

        public FileTransporterContentNotExistException(string message) : base(message)
        {
        }

        public FileTransporterContentNotExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileTransporterContentNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}