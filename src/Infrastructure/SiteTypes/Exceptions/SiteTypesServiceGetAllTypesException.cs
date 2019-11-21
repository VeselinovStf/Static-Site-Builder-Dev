﻿using System;
using System.Runtime.Serialization;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class SiteTypesServiceGetAllTypesException : Exception
    {
        public SiteTypesServiceGetAllTypesException()
        {
        }

        public SiteTypesServiceGetAllTypesException(string message) : base(message)
        {
        }

        public SiteTypesServiceGetAllTypesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SiteTypesServiceGetAllTypesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}