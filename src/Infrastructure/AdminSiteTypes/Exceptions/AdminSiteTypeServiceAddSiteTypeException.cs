using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypes.Exceptions
{
    public class AdminSiteTypeServiceAddSiteTypeException : Exception
    {
        public AdminSiteTypeServiceAddSiteTypeException()
        {
        }

        public AdminSiteTypeServiceAddSiteTypeException(string message) : base(message)
        {
        }

        public AdminSiteTypeServiceAddSiteTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeServiceAddSiteTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
