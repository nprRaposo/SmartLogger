using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogger.Exceptions
{
    public class LogManagerException : Exception
    {
        public LogManagerException()
        {
        }

        public LogManagerException(string message)
            : base(message)
        {
        }

        public LogManagerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
