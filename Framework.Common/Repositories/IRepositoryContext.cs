using System;

using Framework.Common.Domain;

namespace Framework.Common.Repositories
{
    public interface IRepositoryContext : IUnitOfWork, IDisposable
    {
        Guid Id { get; }

        void RegisterNew<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql = null) where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new();

        void RegisterModified<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql = null) where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new();

        void RegisterDeleted<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql = null) where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new();
    }
}