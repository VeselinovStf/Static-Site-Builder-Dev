using System;

namespace Infrastructure.LaunchSite.Exceptions
{
    public class UnLaunchSiteServiceSiteTypeNoSiteTypeException : Exception
    {
        public UnLaunchSiteServiceSiteTypeNoSiteTypeException(string message) : base(message)
        {
        }
    }
}