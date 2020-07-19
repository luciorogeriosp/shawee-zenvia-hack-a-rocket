using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Tropical.Infrastructure.CustomException
{
    public class BusinessException : System.Exception
    {
        public BusinessException()
            : base() { }

        public BusinessException(string message)
            : base(message) { }

        public BusinessException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public BusinessException(string message, System.Exception innerException)
            : base(message, innerException) { }

        public BusinessException(string format, System.Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected BusinessException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
