using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypesServiceConfirmTypeException : Exception
    {
        public SiteTypesServiceConfirmTypeException()
        {
        }

        public SiteTypesServiceConfirmTypeException(string message) : base(message)
        {
        }

        public SiteTypesServiceConfirmTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypesServiceConfirmTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}