using System;
using System.Runtime.Serialization;

namespace Infrastructure.Storage.Exceptions
{
    public class SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception : Exception
    {
        public SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception()
        {
        }

        public SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception(string message) : base(message)
        {
        }

        public SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteStorageCreatorService_StorageCreatorExecute_PushToRepo_Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}