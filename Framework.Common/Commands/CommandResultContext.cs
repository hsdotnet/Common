using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Dapper;

namespace Framework.Common.Commands
{
    public class CommandResultContext
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["OMSWriteDB"].ConnectionString;

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