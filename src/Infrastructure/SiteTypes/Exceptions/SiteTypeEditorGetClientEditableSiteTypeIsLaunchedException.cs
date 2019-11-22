using System;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypeEditorGetClientEditableSiteTypeIsLaunchedException : Exception
    {
        public SiteTypeEditorGetClientEditableSiteTypeIsLaunchedException(string message) : base(message)
        {
        }
    }
}