using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Widgets.Exceptions
{
    public class ManageWidgetsServiceGetAllAsyncException : Exception
    {
        public ManageWidgetsServiceGetAllAsyncException()
        {
        }

        public ManageWidgetsServiceGetAllAsyncException(string message) : base(message)
        {
        }

        public ManageWidgetsServiceGetAllAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ManageWidgetsServiceGetAllAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
