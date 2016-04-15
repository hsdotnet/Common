using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Dapper;

namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令结果上下文对象
    /// </summary>
    public class CommandResultContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["OMSWriteDB"].ConnectionString;

        /// <summary>
        /// 持久化命令对象
        /// </summary>
        /// <typeparam name="TPrimaryKey">主键Id</typeparam>
        /// <param name="commandResult">命令结果对象</param>
        public void AddCommand<TPrimaryKey>(CommandResult<TPrimaryKey> commandResult)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute("INSERT INTO [Sys_Command] ([AggregateRootId], [AggregateRoot], [CommandStatus], [CommandResult], [CommandTime]) VALUES (@AggregateRootId, @AggregateRoot, @Status, @Result, @CommandTime)", commandResult);
            }
        }
    }
}