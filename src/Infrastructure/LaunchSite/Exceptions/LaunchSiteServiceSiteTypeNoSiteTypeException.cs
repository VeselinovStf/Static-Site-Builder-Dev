using System;

namespace Infrastructure.LaunchSite.Exceptions
{
    public class LaunchSiteServiceSiteTypeNoSiteTypeException : Exception
    {
        public LaunchSiteServiceSiteTypeNoSiteTypeException(string message) : base(message)
        {
        }
    }
}