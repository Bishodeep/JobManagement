using System.Runtime.Serialization;

namespace Application.Exceptions
{
    public class BadRequestsException : Exception
    {
        public BadRequestsException()
            : base()
        {
        }

        public BadRequestsException(string message)
            : base(message)
        {
        }

        public BadRequestsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BadRequestsException(string name, object key)
            : base($"Entity \"{name}\" ({key}) Bad Request.")
        {
        }

        protected BadRequestsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
