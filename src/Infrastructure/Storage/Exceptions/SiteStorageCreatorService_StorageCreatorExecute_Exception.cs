using System;
using System.Runtime.Serialization;

namespace Infrastructure.Storage.Exceptions
{
    public class SiteStorageCreatorService_StorageCreatorExecute_Exception : Exception
    {
        public SiteStorageCreatorService_StorageCreatorExecute_Exception()
        {
        }

        public SiteStorageCreatorService_StorageCreatorExecute_Exception(string message) : base(message)
        {
        }

        public SiteStorageCreatorService_StorageCreatorExecute_Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteStorageCreatorService_StorageCreatorExecute_Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}