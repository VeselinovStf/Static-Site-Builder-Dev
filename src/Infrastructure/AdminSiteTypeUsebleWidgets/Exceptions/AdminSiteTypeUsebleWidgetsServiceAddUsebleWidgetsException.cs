using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypeUsebleWidgets.Exceptions
{
    class AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException : Exception
    {
        public AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException()
        {
        }

        public AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException(string message) : base(message)
        {
        }

        public AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeUsebleWidgetsServiceAddUsebleWidgetsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
