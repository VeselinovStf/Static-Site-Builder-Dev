using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.AdminSiteTypeUsebleWidgets.Exceptions
{
    public class AdminSiteTypeUsebleWidgetsServiceSiteTypeWidgetIsNotCompatableException : Exception
    {
        public AdminSiteTypeUsebleWidgetsServiceSiteTypeWidgetIsNotCompatableException()
        {
        }

        public AdminSiteTypeUsebleWidgetsServiceSiteTypeWidgetIsNotCompatableException(string message) : base(message)
        {
        }

        public AdminSiteTypeUsebleWidgetsServiceSiteTypeWidgetIsNotCompatableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdminSiteTypeUsebleWidgetsServiceSiteTypeWidgetIsNotCompatableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
