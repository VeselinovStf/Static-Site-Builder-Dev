using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorEditSiteTypeException : Exception
    {
        public SiteTypeEditorEditSiteTypeException()
        {
        }

        public SiteTypeEditorEditSiteTypeException(string message) : base(message)
        {
        }

        public SiteTypeEditorEditSiteTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypeEditorEditSiteTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}