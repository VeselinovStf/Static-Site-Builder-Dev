using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.Exceptions
{
    public class FileTransporterNewLocationExistsExeption : Exception
    {
        public FileTransporterNewLocationExistsExeption()
        {
        }

        public FileTransporterNewLocationExistsExeption(string message) : base(message)
        {
        }

        public FileTransporterNewLocationExistsExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileTransporterNewLocationExistsExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}