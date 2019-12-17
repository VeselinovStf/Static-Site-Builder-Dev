using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Templates.Exceptions
{
    public class SiteTemplateServiceAddTemplateAsyncException : Exception
    {
        public SiteTemplateServiceAddTemplateAsyncException()
        {
        }

        public SiteTemplateServiceAddTemplateAsyncException(string message) : base(message)
        {
        }

        public SiteTemplateServiceAddTemplateAsyncException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTemplateServiceAddTemplateAsyncException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
