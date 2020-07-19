using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Tropical.Infrastructure.CustomException
{
    public class DatabaseException : System.Exception
    {
        public DatabaseException()
            : base() { }

        public DatabaseException(string message)
            : base(message) { }

        public DatabaseException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DatabaseException(string message, System.Exception innerException)
            : base(message, innerException) { }

        public DatabaseException(string format, System.Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected DatabaseException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
