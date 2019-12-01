using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Widgets.Exceptions
{
    public class WidgetServiceGetAllAsyncException : Exception
    {
        public WidgetServiceGetAllAsyncException()
        {
        }

        public WidgetServiceGetAllAsyncException(string message) : base(message)
        {
        }

        public WidgetServiceGetAllAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WidgetServiceGetAllAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
