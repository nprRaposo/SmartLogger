using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLogger.LogStrategies
{
    public interface ILogger
    {
        void Log(LogConfiguration logConfiguration, LogMessage aMessage);
    }
}
