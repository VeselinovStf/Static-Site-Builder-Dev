using System;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorDeleteSiteTypeNoSiteTypeException : Exception
    {
        public SiteTypeEditorDeleteSiteTypeNoSiteTypeException(string message) : base(message)
        {
        }
    }
}