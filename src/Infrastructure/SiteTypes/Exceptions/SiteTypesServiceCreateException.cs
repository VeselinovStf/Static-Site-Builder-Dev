using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypesServiceCreateException : Exception
    {
        public SiteTypesServiceCreateException()
        {
        }

        public SiteTypesServiceCreateException(string message) : base(message)
        {
        }

        public SiteTypesServiceCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypesServiceCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}