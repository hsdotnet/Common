using System.Collections.Generic;

namespace Framework.Common.QueryServices
{
    public interface IQueryServiceContext
    {
        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <typeparam name="TQueryResult"></typeparam>
        /// <param name="object"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        TQueryResult Get<TQueryResult>(object @object, string sql);

        /// <summary>
        /// 获取记录集合
        /// </summary>
        /// <typeparam name="TQueryResult"></typeparam>
        /// <param name="object"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        IEnumerable<TQueryResult> GetAll<TQueryResult>(object @object, string sql);

        /// <summary>
        /// 分页获取记录
        /// </summary>
        /// <typeparam name="TQueryResult"></typeparam>
        /// <param name="object"></param>
        /// <param name="sql"></param>
        /// <param name="countSql"></param>
        /// <returns></returns>
        PagedResult<TQueryResult> GetAll<TQueryResult>(object @object, string sql, string countSql);
    }
}