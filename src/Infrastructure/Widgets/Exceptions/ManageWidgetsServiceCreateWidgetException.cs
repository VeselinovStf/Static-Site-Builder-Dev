using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Widgets.Exceptions
{
    public class ManageWidgetsServiceCreateWidgetException : Exception
    {
        public ManageWidgetsServiceCreateWidgetException()
        {
        }

        public ManageWidgetsServiceCreateWidgetException(string message) : base(message)
        {
        }

        public ManageWidgetsServiceCreateWidgetException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ManageWidgetsServiceCreateWidgetException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
