using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.Exceptions
{
    public class NetlifyClientPostCreateException : Exception
    {
        public NetlifyClientPostCreateException()
        {
        }

        public NetlifyClientPostCreateException(string message) : base(message)
        {
        }

        public NetlifyClientPostCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NetlifyClientPostCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}