using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.Exceptions
{
    public class NetlifyClientDeployKeysException : Exception
    {
        public NetlifyClientDeployKeysException()
        {
        }

        public NetlifyClientDeployKeysException(string message) : base(message)
        {
        }

        public NetlifyClientDeployKeysException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NetlifyClientDeployKeysException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}