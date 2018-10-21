using System;
using System.Data;
using System.Xml;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.ObjectModel;

namespace GraphQLConsumer
{
    public static class SQLHelper
    {
        static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NorthwindConnection"].ConnectionString;
        const int CommandTimeOut = 600;

        static void AttachParameters(SqlCommand command, SqlParameter[] parameters)
        {
            foreach (SqlParameter parameter in parameters)
            {
                if (parameter != null)
                {
                    if ((parameter.Direction == ParameterDirection.Input) && (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    command.Parameters.Add(parameter);
                }
            }
        }
        /// <summary>
        /// sets the command time out value
        /// </summary>
        /// <param name="command"></param>
        static void PrepareCommand(SqlCommand command)
        {
            if (command != null)
                command.CommandTimeout = CommandTimeOut;
        }
        public static SqlDataReader ExecuteReader(CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (string.IsNullOrEmpty(commandText)) throw new ArgumentNullException("commandText", "Parameter commandText couldn't be null");
            SqlCommand command = new SqlCommand(commandText, new SqlConnection(ConnectionString));
            command.CommandType = commandType;
            if (commandParameters != null)
                AttachParameters(command, commandParameters);
            PrepareCommand(command);
            try
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                command.Parameters.Clear();
                command.Dispose();
                return reader;
            }
            catch (Exception ex)
            {
                //MTSFileLogger.Instance.WriteLog(ex.ToString());
                command.Connection.Close();
                command.Dispose();
                throw;
            }
        }
        public static DataSet ExecuteDataset(string storedProcedure, params SqlParameter[] parameterValues)
        {
            if (string.IsNullOrEmpty(storedProcedure)) throw new ArgumentNullException("storedProcedure", "Parameter storedProcedure couldn't be null");
            DataSet resultDataSet = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter(storedProcedure, ConnectionString);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (parameterValues != null)
                AttachParameters(adapter.SelectCommand, parameterValues);
            PrepareCommand(adapter.SelectCommand);
            try
            {
                adapter.Fill(resultDataSet);
                adapter.SelectCommand.Parameters.Clear();
                adapter.Dispose();
                return resultDataSet;
            }
            catch (Exception ex)
            {
                //MTSFileLogger.Instance.WriteLog(ex.ToString());
                adapter.Dispose();
                throw;
            }
        }

    }
}
