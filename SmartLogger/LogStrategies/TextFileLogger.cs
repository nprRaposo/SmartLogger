using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SmartLogger.LogStrategies
{
    public class TextFileLogger : ILogger
    {
        public void Log(LogConfiguration logConfiguration, LogMessage aMessage)
        {
            try
            {
                var filePath = this.GetFilePath(logConfiguration.FileDirectory);
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(aMessage.MessageToLog);
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region Private Methods
        private string GetFilePath(string path)
        {
            return path + "LogFile" + DateTime.Now.ToShortDateString() + ".txt";
        } 
        #endregion
    }
}
