using System;

using Framework.Common.Domain;

namespace Framework.Common.Commands
{
    public interface ICommandContext : ICommandUnitOfWork, IDisposable
    {
        void Add<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>;

        TPrimaryKey AddAndReturnPrimaryKey<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql) where TAggregateRoot : IEntity<TPrimaryKey>;

        TAggregateRoot Get<TAggregateRoot, TPrimaryKey>(TPrimaryKey id, string sql);
    }
}