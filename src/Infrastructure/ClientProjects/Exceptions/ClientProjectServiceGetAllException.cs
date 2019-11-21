using System;
using System.Runtime.Serialization;

namespace Infrastructure.ClientProjects.Exceptions
{
    public class ClientProjectServiceGetAllException : Exception
    {
        public ClientProjectServiceGetAllException()
        {
        }

        public ClientProjectServiceGetAllException(string message) : base(message)
        {
        }

        public ClientProjectServiceGetAllException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ClientProjectServiceGetAllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}