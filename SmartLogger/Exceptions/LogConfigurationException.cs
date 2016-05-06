using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogger.Exceptions
{
    public class LogConfigurationException : Exception
    {
        public LogConfigurationException()
        {
        }

        public LogConfigurationException(string message)
            : base(message)
        {
        }

        public LogConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
