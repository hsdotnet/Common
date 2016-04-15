using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;

using Dapper;

namespace Framework.Common.QueryServices
{
    public class QueryServiceContext : IQueryServiceContext
    {
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["OMSReadDB"].ConnectionString;

        public TQueryResult Get<TQueryResult>(object @object, string sql)
        {
            return DoQuery<TQueryResult>(connection => { return connection.Query<TQueryResult>(sql, @object).FirstOrDefault(); });
        }

        public IEnumerable<TQueryResult> GetAll<TQueryResult>(object @object, string sql)
        {
            return DoQuery<IEnumerable<TQueryResult>>(connection => { return connection.Query<TQueryResult>(sql, @object); });
        }

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

        private TQueryResult DoQuery<TQueryResult>(Func<IDbConnection, TQueryResult> fun)
        {
            using (IDbConnection connection = new SqlConnection(this._connectionString))
            {
                return fun(connection);
            }
        }
    }
}