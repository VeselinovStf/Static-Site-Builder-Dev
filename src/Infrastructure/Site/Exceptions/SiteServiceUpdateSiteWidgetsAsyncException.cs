using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Site.Exceptions
{
    public class SiteServiceUpdateSiteWidgetsAsyncException : Exception
    {
        public SiteServiceUpdateSiteWidgetsAsyncException()
        {
        }

        public SiteServiceUpdateSiteWidgetsAsyncException(string message) : base(message)
        {
        }

        public SiteServiceUpdateSiteWidgetsAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteServiceUpdateSiteWidgetsAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
