using System;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorEditSiteTypeIsLaunchedException : Exception
    {
        public SiteTypeEditorEditSiteTypeIsLaunchedException(string message) : base(message)
        {
        }
    }
}