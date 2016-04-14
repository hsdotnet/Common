using System;
using System.Threading;
using System.Collections.Generic;

using Framework.Common.Domain;

namespace Framework.Common.Repositories
{
    public abstract class RepositoryContext : DisposableObject, IRepositoryContext
    {
        #region Private Fields
        private readonly Guid id = Guid.NewGuid();

        private readonly ThreadLocal<Dictionary<string, object>> localNewCollection = new ThreadLocal<Dictionary<string, object>>(() => new Dictionary<string, object>());

        private readonly ThreadLocal<Dictionary<string, object>> localModifiedCollection = new ThreadLocal<Dictionary<string, object>>(() => new Dictionary<string, object>());

        private readonly ThreadLocal<Dictionary<string, object>> localDeletedCollection = new ThreadLocal<Dictionary<string, object>>(() => new Dictionary<string, object>());

        private readonly ThreadLocal<bool> localCommitted = new ThreadLocal<bool>(() => true);
        #endregion

        #region IRepositoryContext Members
        public Guid Id
        {
            get { return this.id; }
        }

        public virtual void RegisterNew<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql = null) where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new()
        {
            localNewCollection.Value.Add(sql, aggregateRoot);

            localCommitted.Value = false;
        }

        public virtual void RegisterModified<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql = null) where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new()
        {
            localModifiedCollection.Value.Add(sql, aggregateRoot);

            localCommitted.Value = false;
        }

        public virtual void RegisterDeleted<TAggregateRoot, TPrimaryKey>(TAggregateRoot aggregateRoot, string sql = null) where TAggregateRoot : class, IAggregateRoot<TPrimaryKey>, new()
        {
            localDeletedCollection.Value.Add(sql, aggregateRoot);

            localCommitted.Value = false;
        }
        #endregion

        #region Protected Properties
        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<KeyValuePair<string, object>> NewCollection
        {
            get { return localNewCollection.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<KeyValuePair<string, object>> ModifiedCollection
        {
            get { return localModifiedCollection.Value; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<KeyValuePair<string, object>> DeletedCollection
        {
            get { return localDeletedCollection.Value; }
        }
        #endregion

        #region DisposableObject Members
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.localCommitted.Dispose();
                this.localDeletedCollection.Dispose();
                this.localModifiedCollection.Dispose();
                this.localNewCollection.Dispose();
            }
        }
        #endregion

        protected abstract void DoCommit();

        protected abstract void DoRollback();

        #region IUnitOfWork Members
        public bool Committed
        {
            get { return this.localCommitted.Value; }
            protected set { this.localCommitted.Value = value; }
        }

        public virtual bool DistributedTransactionSupported
        {
            get { return false; }
        }

        public void Commit()
        {
            this.DoCommit();
        }

        public void Rollback()
        {
            this.DoRollback();
        }
        #endregion
    }
}