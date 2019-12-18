using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Infrastructure.Tutorial.Exceptions
{
    public class TutorialServiceGIsClientInTutorialException : Exception
    {
        public TutorialServiceGIsClientInTutorialException()
        {
        }

        public TutorialServiceGIsClientInTutorialException(string message) : base(message)
        {
        }

        public TutorialServiceGIsClientInTutorialException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TutorialServiceGIsClientInTutorialException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
