using System;
using System.Runtime.Serialization;

namespace WindowChrome.Demo
{
    [Serializable]
    internal class NameAlreadyExistsException : Exception
    {
        public NameAlreadyExistsException()
        {
        }

        public NameAlreadyExistsException(string message) : base(message)
        {
        }

        public NameAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}