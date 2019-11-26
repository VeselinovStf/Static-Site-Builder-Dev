using System;
using System.Runtime.Serialization;

namespace Infrastructure.Templates.Exceptions
{
    public class TemplateServiceGetAllException : Exception
    {
        public TemplateServiceGetAllException()
        {
        }

        public TemplateServiceGetAllException(string message) : base(message)
        {
        }

        public TemplateServiceGetAllException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TemplateServiceGetAllException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}