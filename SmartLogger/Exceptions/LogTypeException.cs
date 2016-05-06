using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogger.Exceptions
{
    public class LogTypeException : Exception
    {
        public LogTypeException()
        {
        }

        public LogTypeException(string message)
            : base(message)
        {
        }

        public LogTypeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
