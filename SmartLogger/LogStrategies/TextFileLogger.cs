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

        #region Properties
        private string FilePath { get; set; } 
        #endregion

        public TextFileLogger()
        {
            this.FilePath = ConfigurationManager.AppSettings["LogFileDirectory"] + "LogFile" + DateTime.Now.ToShortDateString() + ".txt";
        }
        
        public void Log(LogMessage aMessage)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(this.FilePath))
                {
                    sw.WriteLine(aMessage.MessageToLog);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
