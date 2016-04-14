using System.Collections.Generic;

using Framework.Common.Domain;

namespace Framework.Common.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TAggregateRoot"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class Repository<TAggregateRoot, TPrimaryKey> : IRepository<TAggregateRoot, TPrimaryKey>
         where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new()
    {
        private IRepositoryContext context;

        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        public void Add(TAggregateRoot aggregateRoot)
        {
            this.DoAdd(aggregateRoot);
        }

        public void Update(TAggregateRoot aggregateRoot)
        {
            this.DoUpdate(aggregateRoot);
        }

        public void Delete(TAggregateRoot aggregateRoot)
        {
            this.DoDelete(aggregateRoot);
        }

        public TAggregateRoot GetById(TPrimaryKey id)
        {
            return this.DoGetById(id);
        }

        public IEnumerable<TAggregateRoot> GetAll()
        {
            return this.DoGetAll();
        }

        protected abstract void DoAdd(TAggregateRoot aggregateRoot);

        protected abstract void DoUpdate(TAggregateRoot aggregateRoot);

        protected abstract void DoDelete(TAggregateRoot aggregateRoot);

        protected abstract TAggregateRoot DoGetById(TPrimaryKey id);

        protected abstract IEnumerable<TAggregateRoot> DoGetAll();
    }
}