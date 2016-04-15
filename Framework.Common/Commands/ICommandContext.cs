using System;

using Framework.Common.Domain;

namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令上下文接口
    /// </summary>
    public interface ICommandContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 增删改操作
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合跟对象类型</typeparam>
        /// <typeparam name="TPrimaryKey">聚合跟对象主键类型</typeparam>
        /// <param name="aggregateRoot">聚合跟对象</param>
        /// <param name="sql">执行的sql语句</param>
        void Add<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>;

        /// <summary>
        /// 返回首行首列对象
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合跟对象类型</typeparam>
        /// <typeparam name="TPrimaryKey">聚合跟对象主键类型</typeparam>
        /// <param name="aggregateRoot">聚合跟对象</param>
        /// <param name="sql">执行的sql语句</param>
        /// <returns>对象类型</returns>
        TPrimaryKey AddAndReturnPrimaryKey<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>;

        /// <summary>
        /// 获取聚合跟对象
        /// </summary>
        /// <typeparam name="TAggregateRoot">聚合跟对象类型</typeparam>
        /// <typeparam name="TPrimaryKey">聚合跟对象主键类型</typeparam>
        /// <param name="id">主键Id</param>
        /// <param name="sql">执行的sql语句</param>
        /// <returns>聚合跟对象</returns>
        TAggregateRoot Get<TAggregateRoot, TPrimaryKey>(TPrimaryKey id, string sql);
    }
}