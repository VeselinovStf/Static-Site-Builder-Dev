using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorGetClientEditableSiteTypeException : Exception
    {
        public SiteTypeEditorGetClientEditableSiteTypeException()
        {
        }

        public SiteTypeEditorGetClientEditableSiteTypeException(string message) : base(message)
        {
        }

        public SiteTypeEditorGetClientEditableSiteTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypeEditorGetClientEditableSiteTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}