using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypesServiceGetBuildInSiteTypesException : Exception
    {
        public SiteTypesServiceGetBuildInSiteTypesException()
        {
        }

        public SiteTypesServiceGetBuildInSiteTypesException(string message) : base(message)
        {
        }

        public SiteTypesServiceGetBuildInSiteTypesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypesServiceGetBuildInSiteTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
