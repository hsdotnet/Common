using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

using Dapper;
using Framework.Common.Domain;

namespace Framework.Common.Commands
{
    public class CommandContext : ICommandContext
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["OMSWriteDB"].ConnectionString;

        private readonly IDbConnection _connection;

        private bool _isOpenTransaction = false;

        private IDbTransaction _transaction;

        public CommandContext()
        {
            if (this._connection == null)
                this._connection = new SqlConnection(this._connectionString);
        }

        public void Add<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>
        {
            this.OpenTransaction();

            this._connection.Execute(sql, aggregateRoot, this._transaction);
        }

        public TPrimaryKey AddAndReturnPrimaryKey<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>
        {
            this.OpenTransaction();

            return this._connection.ExecuteScalar<TPrimaryKey>(sql, aggregateRoot, this._transaction);
        }

        public TAggregateRoot Get<TAggregateRoot, TPrimaryKey>(TPrimaryKey id, string sql)
        {
            return this._connection.Query<TAggregateRoot>(sql, new { Id = id }).FirstOrDefault();
        }

        private void OpenTransaction()
        {
            if (!this._isOpenTransaction)
            {
                if (string.IsNullOrWhiteSpace(this._connection.ConnectionString))
                    this._connection.ConnectionString = this._connectionString;

                this._connection.Open();

                this._transaction = this._connection.BeginTransaction();

                this._isOpenTransaction = true;
            }

            this.Committed = false;
        }

        public void Commit()
        {
            if (this._transaction != null && !this.Committed)
            {
                this._transaction.Commit();

                this.Committed = true;
            }
        }

        public void Rollback()
        {
            if (this._transaction != null)
                this._transaction.Rollback();

            this.Committed = false;
        }

        public void Dispose()
        {
            if (this._transaction != null)
                this._transaction.Dispose();

            this._connection.Close();
            this._connection.Dispose();

            this._isOpenTransaction = false;

            this.Committed = true;
        }

        public bool DistributedTransactionSupported
        {
            get { return true; }
        }

        public bool Committed
        {
            get;
            private set;
        }
    }
}