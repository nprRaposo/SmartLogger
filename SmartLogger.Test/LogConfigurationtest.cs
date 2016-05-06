using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SmartLogger.Test
{
    [TestClass]
    public class LogConfigurationTest
    {
        [TestMethod]
        public void LogConfiguration_Object_read_correctly_logLevels_from_App_config()
        {
            var logConfiguration = new LogConfiguration();
            Assert.IsTrue(logConfiguration.LogLevels.Any(ll => ll.ToLower().Contains("message")));
            Assert.IsTrue(logConfiguration.LogLevels.Any(ll => ll.ToLower().Contains("error")));
            Assert.IsFalse(logConfiguration.LogLevels.Any(ll => ll.ToLower().Contains("Message")));
        }

        [TestMethod]
        public void LogConfiguration_Object_read_correctly_logtypes_from_App_config()
        {
            var logConfiguration = new LogConfiguration();
            Assert.IsTrue(logConfiguration.LogTypes.Any(ll => ll.ToLower().Contains("sql")));
            Assert.IsTrue(logConfiguration.LogTypes.Any(ll => ll.ToLower().Contains("file")));
            Assert.IsFalse(logConfiguration.LogTypes.Any(ll => ll.ToLower().Contains("console")));
        }

        [TestMethod]
        public void LogConfiguration_Object_read_correctly_connectionString_from_App_config()
        {
            var logConfiguration = new LogConfiguration();
            Assert.AreEqual("aConnectionString", logConfiguration.ConnectionString);
        }

        [TestMethod]
        public void LogConfiguration_Object_read_correctly_LogFileDirectory_from_App_config()
        {
            var logConfiguration = new LogConfiguration();
            Assert.AreEqual("C:\\Test\\", logConfiguration.FileDirectory);
        }
    }
}
