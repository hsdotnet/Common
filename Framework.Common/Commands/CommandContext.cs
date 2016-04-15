using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Configuration;

using Dapper;
using Framework.Common.Domain;

namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令上下文对象
    /// </summary>
    public class CommandContext : ICommandContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["OMSWriteDB"].ConnectionString;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private readonly IDbConnection _connection;

        /// <summary>
        /// 是否已开启事务
        /// </summary>
        private bool _isOpenTransaction = false;

        /// <summary>
        /// 事务对象
        /// </summary>
        private IDbTransaction _transaction;

        /// <summary>
        /// 构造函数
        /// 初始化数据库连接对象
        /// </summary>
        public CommandContext()
        {
            if (this._connection == null)
                this._connection = new SqlConnection(this._connectionString);
        }

        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合跟对象类型</typeparam>
        /// <typeparam name="TPrimaryKey">聚合跟对象主键类型</typeparam>
        /// <param name="aggregateRoot">聚合跟对象</param>
        /// <param name="sql">执行的Sql语句</param>
        public void Add<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>
        {
            this.OpenTransaction();

            this._connection.Execute(sql, aggregateRoot, this._transaction);
        }

        /// <summary>
        /// 返回首行首列对象
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合跟对象类型</typeparam>
        /// <typeparam name="TPrimaryKey">聚合跟对象主键类型</typeparam>
        /// <param name="aggregateRoot">聚合跟对象</param>
        /// <param name="sql">执行的Sql语句</param>
        /// <returns>对象类型</returns>
        public TPrimaryKey AddAndReturnPrimaryKey<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>
        {
            this.OpenTransaction();

            return this._connection.ExecuteScalar<TPrimaryKey>(sql, aggregateRoot, this._transaction);
        }

        /// <summary>
        /// 获取聚合跟对象
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合跟对象类型</typeparam>
        /// <typeparam name="TPrimaryKey">聚合跟对象主键类型</typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="sql">执行的Sql语句</param>
        /// <returns>聚合跟对象</returns>
        public TAggregateRoot Get<TAggregateRoot, TPrimaryKey>(TPrimaryKey id, string sql)
        {
            return this._connection.Query<TAggregateRoot>(sql, new { Id = id }).FirstOrDefault();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
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

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (this._transaction != null && !this.Committed)
            {
                this._transaction.Commit();

                this.Committed = true;
            }
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (this._transaction != null)
                this._transaction.Rollback();

            this.Committed = false;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        public void Dispose()
        {
            if (this._transaction != null)
                this._transaction.Dispose();

            this._connection.Close();
            this._connection.Dispose();

            this._isOpenTransaction = false;

            this.Committed = true;
        }

        /// <summary>
        /// 是否在当前的工作单元模式中使用分布式事物 Transaction Coordinator (MS-DTC).
        /// </summary>
        public bool DistributedTransactionSupported
        {
            get { return true; }
        }

        /// <summary>
        /// 当前的Unit Of Work事务是否已被提交。
        /// </summary>
        public bool Committed
        {
            get;
            private set;
        }
    }
}