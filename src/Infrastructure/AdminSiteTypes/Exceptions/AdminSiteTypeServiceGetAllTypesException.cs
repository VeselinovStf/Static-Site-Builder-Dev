using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypes.Exceptions
{
    public class AdminSiteTypeServiceGetAllTypesException : Exception
    {
        public AdminSiteTypeServiceGetAllTypesException()
        {
        }

        public AdminSiteTypeServiceGetAllTypesException(string message) : base(message)
        {
        }

        public AdminSiteTypeServiceGetAllTypesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeServiceGetAllTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
