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
        public virtual void Log(LogConfiguration logConfiguration, LogMessage aMessage)
        {
            using (var connection = new SqlConnection(logConfiguration.ConnectionString))
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
    }
}
