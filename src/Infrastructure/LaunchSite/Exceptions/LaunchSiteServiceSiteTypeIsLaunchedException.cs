using System;

namespace Infrastructure.LaunchSite.Exceptions
{
    public class LaunchSiteServiceSiteTypeIsLaunchedException : Exception
    {
        public LaunchSiteServiceSiteTypeIsLaunchedException(string message) : base(message)
        {
        }
    }
}