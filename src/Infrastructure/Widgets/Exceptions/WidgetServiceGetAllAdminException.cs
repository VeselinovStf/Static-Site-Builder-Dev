using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Widgets.Exceptions
{
    public class WidgetServiceGetAllAdminException : Exception
    {
        public WidgetServiceGetAllAdminException()
        {
        }

        public WidgetServiceGetAllAdminException(string message) : base(message)
        {
        }

        public WidgetServiceGetAllAdminException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WidgetServiceGetAllAdminException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
