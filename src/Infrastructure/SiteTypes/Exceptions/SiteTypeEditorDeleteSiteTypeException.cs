using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorDeleteSiteTypeException : Exception
    {
        public SiteTypeEditorDeleteSiteTypeException()
        {
        }

        public SiteTypeEditorDeleteSiteTypeException(string message) : base(message)
        {
        }

        public SiteTypeEditorDeleteSiteTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypeEditorDeleteSiteTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}