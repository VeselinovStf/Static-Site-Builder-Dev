using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Site.Exceptions
{
    public class SiteServiceRenderSiteException : Exception
    {
        public SiteServiceRenderSiteException()
        {
        }

        public SiteServiceRenderSiteException(string message) : base(message)
        {
        }

        public SiteServiceRenderSiteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteServiceRenderSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
