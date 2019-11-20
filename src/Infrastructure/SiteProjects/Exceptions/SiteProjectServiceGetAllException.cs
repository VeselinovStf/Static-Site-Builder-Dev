using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteProjects.Exceptions
{
    public class SiteProjectServiceGetAllException : Exception
    {
        public SiteProjectServiceGetAllException()
        {
        }

        public SiteProjectServiceGetAllException(string message) : base(message)
        {
        }

        public SiteProjectServiceGetAllException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteProjectServiceGetAllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}