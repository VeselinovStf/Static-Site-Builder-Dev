using System;
using System.Runtime.Serialization;

namespace Infrastructure.LaunchSite.Exceptions
{
    public class LaunchSiteServiceLaunchSiteException : Exception
    {
        public LaunchSiteServiceLaunchSiteException()
        {
        }

        public LaunchSiteServiceLaunchSiteException(string message) : base(message)
        {
        }

        public LaunchSiteServiceLaunchSiteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LaunchSiteServiceLaunchSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}