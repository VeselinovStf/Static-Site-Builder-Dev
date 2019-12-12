using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Widgets.Exceptions
{
    public class WidgetServiceGetBuildInWidgetTypesException : Exception
    {
        public WidgetServiceGetBuildInWidgetTypesException()
        {
        }

        public WidgetServiceGetBuildInWidgetTypesException(string message) : base(message)
        {
        }

        public WidgetServiceGetBuildInWidgetTypesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WidgetServiceGetBuildInWidgetTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
