using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.SiteTypes.Exceptions
{
    public class NotEnoughtDiamondsSiteTypeServiceException_Note : Exception
    {
        public NotEnoughtDiamondsSiteTypeServiceException_Note()
        {
        }

        public NotEnoughtDiamondsSiteTypeServiceException_Note(string message) : base(message)
        {
        }

        public NotEnoughtDiamondsSiteTypeServiceException_Note(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotEnoughtDiamondsSiteTypeServiceException_Note(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
