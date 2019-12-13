using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypeWidgets.Exceptions
{
    public class AdminSiteTypeWidgetsServiceGetSiteTypeException : Exception
    {
        public AdminSiteTypeWidgetsServiceGetSiteTypeException()
        {
        }

        public AdminSiteTypeWidgetsServiceGetSiteTypeException(string message) : base(message)
        {
        }

        public AdminSiteTypeWidgetsServiceGetSiteTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeWidgetsServiceGetSiteTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
