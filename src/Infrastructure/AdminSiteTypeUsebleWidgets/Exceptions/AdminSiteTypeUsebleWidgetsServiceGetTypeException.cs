using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypeUsebleWidgets.Exceptions
{
    public class AdminSiteTypeUsebleWidgetsServiceGetTypeException : Exception
    {
        public AdminSiteTypeUsebleWidgetsServiceGetTypeException()
        {
        }

        public AdminSiteTypeUsebleWidgetsServiceGetTypeException(string message) : base(message)
        {
        }

        public AdminSiteTypeUsebleWidgetsServiceGetTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeUsebleWidgetsServiceGetTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
