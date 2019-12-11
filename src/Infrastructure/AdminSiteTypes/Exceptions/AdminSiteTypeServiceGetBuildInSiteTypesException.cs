using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypes.Exceptions
{
    public class AdminSiteTypeServiceGetBuildInSiteTypesException : Exception
    {
        public AdminSiteTypeServiceGetBuildInSiteTypesException()
        {
        }

        public AdminSiteTypeServiceGetBuildInSiteTypesException(string message) : base(message)
        {
        }

        public AdminSiteTypeServiceGetBuildInSiteTypesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeServiceGetBuildInSiteTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
