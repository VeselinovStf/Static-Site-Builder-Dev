using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Widgets.Exceptions
{
    public class ManageWidgetsServiceAddWidgetException : Exception
    {
        public ManageWidgetsServiceAddWidgetException()
        {
        }

        public ManageWidgetsServiceAddWidgetException(string message) : base(message)
        {
        }

        public ManageWidgetsServiceAddWidgetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ManageWidgetsServiceAddWidgetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
