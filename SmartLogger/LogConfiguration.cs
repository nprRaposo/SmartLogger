using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using SmartLogger.Exceptions;


namespace SmartLogger
{
    public class LogConfiguration
    {
        #region Properties
        public virtual IEnumerable<string> LogLevels { get; set; }
        public virtual IEnumerable<string> LogTypes { get; set; }
        public virtual string ConnectionString { get; set; }
        public virtual string FileDirectory { get; set; }
        #endregion

        public LogConfiguration()
        {
            this.LogLevels = this.GetLevels();
            this.LogTypes = this.GetLogTypes();
            this.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
            this.FileDirectory = ConfigurationManager.AppSettings["LogFileDirectory"];
        }

        #region Private Methods
        private IEnumerable<string> GetLevels()
        {
            return this.ReadFromConfig("LogLevel");
        }

        private IEnumerable<string> GetLogTypes()
        {
            return this.ReadFromConfig("LogType");
        } 

        private IEnumerable<string> ReadFromConfig(string key)
        {
            try
            {
                var result = ConfigurationManager.AppSettings[key];
                return result.Split(',').ToList();
            }
            catch (Exception ex)
            {
                throw new LogConfigurationException(ex.Message, ex);
            }
        }
        #endregion
    }
}
