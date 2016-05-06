﻿using SmartLogger.LogStrategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SmartLogger
{
    public class LogManager
    {
        #region Properties
        private IEnumerable<ILogger> LoggerStrategies { get; set; }
        private IEnumerable<LogLevel> LoggerLevels { get; set; } 
        #endregion

        #region Constructor
        public LogManager()
        {
            var logConfiguration = new LogConfiguration();
            this.Setup(logConfiguration);
        } 
        #endregion

        public void Message(string aMessage)
        {
            this.WriteLog(aMessage, LogLevel.Message);
        }

        public void Error(string aMessage)
        {
            this.WriteLog(aMessage, LogLevel.Error);
        }

        public void Warning (string aMessage)
        {
            this.WriteLog(aMessage, LogLevel.Warning);
        }

        #region Private Methods
        private void WriteLog(string message, LogLevel level)
        {
            var logMessage = new LogMessage
            {
                Level = level,
                Message = message
            };

            if (!this.HasToBeLogged(logMessage))
                return;

            foreach (var logStrategy in this.LoggerStrategies)
            {
                logStrategy.Log(logMessage);
            }
        }

        public void Setup(LogConfiguration loggerConfiguration)
        {
            this.LoggerLevels = this.GetLevelsFromConfig(loggerConfiguration.LogLevels);
            this.LoggerStrategies = this.GetLogStrategiesFromConfig(loggerConfiguration.LogTypes);
        } 

        private IEnumerable<LogLevel> GetLevelsFromConfig(IEnumerable<string> levels)
        {
            var loggerLevels = new List<LogLevel>();

            if (levels.Any(l => l.ToLower().Contains("error")))
                loggerLevels.Add(LogLevel.Error);
            if (levels.Any(l => l.ToLower().Contains("message")))
                loggerLevels.Add(LogLevel.Message);
            if (levels.Any(l => l.ToLower().Contains("warning")))
                loggerLevels.Add(LogLevel.Warning);

            return loggerLevels;
        }

        private IEnumerable<ILogger> GetLogStrategiesFromConfig(IEnumerable<string> logTypes)
        {
            var loggerStrategies = new List<ILogger>();

            if (logTypes.Any(l => l.ToLower().Contains("sql")))
                loggerStrategies.Add(new SqlLogger());
            if (logTypes.Any(l => l.ToLower().Contains("console")))
                loggerStrategies.Add(new ConsoleLogger());
            if (logTypes.Any(l => l.ToLower().Contains("file")))
                loggerStrategies.Add(new TextFileLogger());

            return loggerStrategies;
        }

        private bool HasToBeLogged(LogMessage aLogMessage)
        {
            return this.LoggerLevels.Contains(aLogMessage.Level);
        }
        #endregion
    }
}