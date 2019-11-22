using System;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorEditSiteTypeNoSiteTypeException : Exception
    {
        public SiteTypeEditorEditSiteTypeNoSiteTypeException(string message) : base(message)
        {
        }
    }
}