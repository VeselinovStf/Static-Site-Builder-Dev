using System;

namespace Infrastructure.LaunchSite.Exceptions
{
    public class UnLaunchSiteServiceSiteTypeIsNotLaunchedException : Exception
    {
        public UnLaunchSiteServiceSiteTypeIsNotLaunchedException(string message) : base(message)
        {
        }
    }
}