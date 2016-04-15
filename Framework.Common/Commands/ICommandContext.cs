using System;

using Framework.Common.Domain;

namespace Framework.Common.Commands
{
    public interface ICommandContext : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="aggregateRoot"></param>
        /// <param name="sql"></param>
        void Add<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="aggregateRoot"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        TPrimaryKey AddAndReturnPrimaryKey<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregateRoot"></typeparam>
        /// <typeparam name="TPrimaryKey"></typeparam>
        /// <param name="id"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        TAggregateRoot Get<TAggregateRoot, TPrimaryKey>(TPrimaryKey id, string sql);
    }
}