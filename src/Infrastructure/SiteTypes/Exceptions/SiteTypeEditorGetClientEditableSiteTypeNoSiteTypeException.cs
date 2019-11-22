using System;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorGetClientEditableSiteTypeNoSiteTypeException : Exception
    {
        public SiteTypeEditorGetClientEditableSiteTypeNoSiteTypeException(string message) : base(message)
        {
        }
    }
}