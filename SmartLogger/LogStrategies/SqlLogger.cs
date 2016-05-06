using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace SmartLogger.LogStrategies
{
    public class SqlLogger : ILogger
    {

        #region Properties
        private string ConnectionString { get; set; } 
        #endregion

        public SqlLogger()
        {
            this.ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
        }

        public void Log(LogMessage aMessage)
        {
            using (var connection = new SqlConnection(this.ConnectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                var transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = "Insert into Log VALUES (" + aMessage.Message + "," + aMessage.Level.ToString() + ");";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception anotherEx)
                    {
                    }
                }
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
