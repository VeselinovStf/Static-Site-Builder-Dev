using System;
using System.Runtime.Serialization;

namespace Infrastructure.Templates.Exceptions
{
    public class TemplateServiceConfigureCiCdVariablesException : Exception
    {
        public TemplateServiceConfigureCiCdVariablesException()
        {
        }

        public TemplateServiceConfigureCiCdVariablesException(string message) : base(message)
        {
        }

        public TemplateServiceConfigureCiCdVariablesException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TemplateServiceConfigureCiCdVariablesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}