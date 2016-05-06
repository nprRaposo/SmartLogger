using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartLogger.LogStrategies
{
    public class ConsoleLogger : ILogger
    {
        public void Log(LogMessage aMessage)
        {
            try
            {
                Console.BackgroundColor = this.GetBackGroundFor(aMessage);
                Console.WriteLine(aMessage.MessageToLog);
            }
            catch (Exception ex)
            {
            }
        }

        #region Private Methods
        private ConsoleColor GetBackGroundFor(LogMessage aLogMessage)
        {
            switch (aLogMessage.Level)
            {
                case LogLevel.Error:
                    {
                        return ConsoleColor.Red;
                        break;
                    }
                case LogLevel.Message:
                    {
                        return ConsoleColor.White;
                        break;
                    }
                case LogLevel.Warning:
                    {
                        return ConsoleColor.Yellow;
                        break;
                    }
                default:
                    return ConsoleColor.Black;
            }
        } 
        #endregion
    }
}
