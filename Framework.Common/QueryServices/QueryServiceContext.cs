using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;

using Dapper;

namespace Framework.Common.QueryServices
{
    /// <summary>
    /// 查询服务上下文
    /// </summary>
    public class QueryServiceContext : IQueryServiceContext
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["QueryDB"].ConnectionString;

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="object">参数对象</param>
        /// <param name="sql">Sql语句</param>
        /// <returns>结果对象</returns>
        public TQueryResult Get<TQueryResult>(object @object, string sql)
        {
            return DoQuery<TQueryResult>(connection => { return connection.Query<TQueryResult>(sql, @object).FirstOrDefault(); });
        }

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="object">参数对象</param>
        /// <param name="sql">Sql语句</param>
        /// <returns>结果对象集合</returns>
        public IEnumerable<TQueryResult> GetAll<TQueryResult>(object @object, string sql)
        {
            return DoQuery<IEnumerable<TQueryResult>>(connection => { return connection.Query<TQueryResult>(sql, @object); });
        }

        /// <summary>
        /// 分页获取记录
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="object">参数对象</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="countSql">获取条数Sql语句</param>
        /// <returns>分页对象</returns>
        public PagedResult<TQueryResult> GetAll<TQueryResult>(object @object, string sql, string countSql)
        {
            return DoQuery<PagedResult<TQueryResult>>(connection =>
            {
                PagedResult<TQueryResult> pagedResult = new PagedResult<TQueryResult>();

                pagedResult.Items = connection.Query<TQueryResult>(sql, @object);

                pagedResult.TotalItems = connection.Query<int>(countSql, @object).FirstOrDefault();

                return pagedResult;
            });
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="fun">Func<IDbConnection, TQueryResult></param>
        /// <returns>结果对象</returns>
        private TQueryResult DoQuery<TQueryResult>(Func<IDbConnection, TQueryResult> fun)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                return fun(connection);
            }
        }
    }
}