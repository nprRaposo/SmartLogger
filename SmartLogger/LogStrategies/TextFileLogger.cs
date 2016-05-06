using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SmartLogger.Exceptions;

namespace SmartLogger.LogStrategies
{
    public class TextFileLogger : ILogger
    {
        public virtual void Log(LogConfiguration logConfiguration, LogMessage aMessage)
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
                throw new LogTypeException(ex.Message, ex);
            }
        }

        #region Private Methods
        private string GetFilePath(string path)
        {
            var sb = new StringBuilder();
            sb.Append(path)
                .Append("LogFile")
                .Append(DateTime.Now.ToShortDateString())
                .Append(".txt");

            return sb.ToString();
        } 
        #endregion
    }
}
