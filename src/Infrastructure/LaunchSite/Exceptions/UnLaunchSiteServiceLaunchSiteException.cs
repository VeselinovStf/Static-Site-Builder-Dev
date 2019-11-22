using System;
using System.Runtime.Serialization;

namespace Infrastructure.LaunchSite.Exceptions
{
    public class UnLaunchSiteServiceLaunchSiteException : Exception
    {
        public UnLaunchSiteServiceLaunchSiteException()
        {
        }

        public UnLaunchSiteServiceLaunchSiteException(string message) : base(message)
        {
        }

        public UnLaunchSiteServiceLaunchSiteException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnLaunchSiteServiceLaunchSiteException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}