using System;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorDeleteSiteTypeIsLaunchedException : Exception
    {
        public SiteTypeEditorDeleteSiteTypeIsLaunchedException(string message) : base(message)
        {
        }
    }
}