using System.Collections.Generic;

using Framework.Common.Domain;

namespace Framework.Common.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IRepository<TAggregateRoot, TPrimaryKey> where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new()
    {
        IRepositoryContext Context { get; }

        void Add(TAggregateRoot aggregateRoot);

        void Update(TAggregateRoot aggregateRoot);

        void Delete(TAggregateRoot aggregateRoot);

        TAggregateRoot GetById(TPrimaryKey id);

        IEnumerable<TAggregateRoot> GetAll();
    }
}