using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using SmartLogger.LogStrategies;

namespace SmartLogger.Test
{
    [TestClass]
    public class LogManagerTest
    {
        [TestMethod]
        public void LogManager_Correctly_Set_LogLevel_From_LogConfiguration()
        {
            var logConfigurationMock = new Mock<LogConfiguration>();
            logConfigurationMock.Setup(lc => lc.LogLevels).Returns(new List<string>() { "message", "error" });

            var logManager = new LogManager(logConfigurationMock.Object);

            Assert.IsTrue(logManager.LoggerLevels.Any(ll => ll == LogLevel.Message));
            Assert.IsTrue(logManager.LoggerLevels.Any(ll => ll == LogLevel.Error));
            Assert.IsFalse(logManager.LoggerLevels.Any(ll => ll == LogLevel.Warning));
        }

        [TestMethod]
        public void LogManager_Correctly_Set_LogStrategies_From_LogConfiguration_With_Sql_Console_File_Logger()
        {
            var logConfigurationMock = new Mock<LogConfiguration>();
            logConfigurationMock.Setup(lc => lc.LogTypes).Returns(new List<string>() { "sql", "console", "file" });

            var logManager = new LogManager(logConfigurationMock.Object);

            Assert.IsTrue(logManager.LoggerStrategies.OfType<SqlLogger>().Count() > 0);
            Assert.IsTrue(logManager.LoggerStrategies.OfType<ConsoleLogger>().Count() > 0);
            Assert.IsTrue(logManager.LoggerStrategies.OfType<TextFileLogger>().Count() > 0);
        }

        [TestMethod]
        public void LogManager_Correctly_No_Set_LogStrategies_From_LogConfiguration_With_There_Is_No_Logger_Strategy()
        {
            var logConfigurationMock = new Mock<LogConfiguration>();
            logConfigurationMock.Setup(lc => lc.LogTypes).Returns(new List<string>() {  });

            var logManager = new LogManager(logConfigurationMock.Object);

            Assert.IsFalse(logManager.LoggerStrategies.OfType<SqlLogger>().Count() > 0);
            Assert.IsFalse(logManager.LoggerStrategies.OfType<ConsoleLogger>().Count() > 0);
            Assert.IsFalse(logManager.LoggerStrategies.OfType<TextFileLogger>().Count() > 0);
        }

        [TestMethod]
        public void HasToBeLogged_Is_False_When_LogMessageLevel_Is_Not_Configured()
        {
            var logConfigurationMock = new Mock<LogConfiguration>();
            logConfigurationMock.Setup(lc => lc.LogLevels).Returns(new List<string>() { "error" });

            var logManager = new LogManager(logConfigurationMock.Object);
            var message = new LogMessage
            {
                Level = LogLevel.Message
            };

            Assert.IsFalse(logManager.HasToBeLogged(message));
        }

        [TestMethod]
        public void HasToBeLogged_Is_True_When_LogMessageLevel_Is_Configured()
        {
            var logConfigurationMock = new Mock<LogConfiguration>();
            logConfigurationMock.Setup(lc => lc.LogLevels).Returns(new List<string>() { "error" });

            var logManager = new LogManager(logConfigurationMock.Object);
            var message = new LogMessage
            {
                Level = LogLevel.Error
            }; 

            Assert.IsTrue(logManager.HasToBeLogged(message));
        }

        [TestMethod]
        public void LogManager_correctly_call_Log_Strategies_Configured_To_Log()
        {
            var logConfigurationMock = new Mock<LogConfiguration>();
            logConfigurationMock.Setup(lc => lc.LogLevels).Returns(new List<string>() { "error" });
            var sqlLoggerMock = new Mock<SqlLogger>();
            var consoleLoggerMock = new Mock<ConsoleLogger>();

            var logManager = new LogManager(logConfigurationMock.Object);
            logManager.LoggerStrategies = new List<ILogger>() { sqlLoggerMock.Object, consoleLoggerMock.Object};
            logManager.Error(string.Empty);
            
            sqlLoggerMock.Verify(s => s.Log(logConfigurationMock.Object, It.IsAny<LogMessage>()), Times.Once);
            consoleLoggerMock.Verify(s => s.Log(logConfigurationMock.Object, It.IsAny<LogMessage>()), Times.Once);
        }
    }
}
