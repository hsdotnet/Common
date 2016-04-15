using System.Linq;
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
        /// <param name="commandResult">命令结果对象</param>
        public void AddCommand(CommandResult commandResult)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                connection.Open();

                connection.Execute("INSERT INTO [Sys_Command] ([AggregateRootId], [AggregateRoot], [CommandStatus], [CommandResult], [CommandTime]) VALUES (@AggregateRootId, @AggregateRoot, @Status, @Result, @CommandTime)", commandResult);
            }
        }

        /// <summary>
        /// 获取命令结果对象
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns>命令结果对象</returns>
        public CommandResult Get(long id)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                return connection.Query<CommandResult>("SELECT AggregateRootId,AggregateRoot,CommandStatus [Status],CommandResult Result,CommandTime,CreateTime FROM Sys_Command WHERE CommandId = @Id", new { Id = id }).FirstOrDefault();
            }
        }
    }
}