using System.Collections.Generic;

namespace Framework.Common.QueryServices
{
    /// <summary>
    /// 查询服务上下文接口
    /// </summary>
    public interface IQueryServiceContext
    {
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="object">参数对象</param>
        /// <param name="sql">Sql语句</param>
        /// <returns>结果对象</returns>
        TQueryResult Get<TQueryResult>(object @object, string sql);

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="object">参数对象</param>
        /// <param name="sql">Sql语句</param>
        /// <returns>结果对象集合</returns>
        IEnumerable<TQueryResult> GetAll<TQueryResult>(object @object, string sql);

        /// <summary>
        /// 分页获取记录
        /// </summary>
        /// <typeparam name="TQueryResult">结果对象</typeparam>
        /// <param name="object">参数对象</param>
        /// <param name="sql">Sql语句</param>
        /// <param name="countSql">获取条数Sql语句</param>
        /// <returns>分页对象</returns>
        PagedResult<TQueryResult> GetAll<TQueryResult>(object @object, string sql, string countSql);
    }
}