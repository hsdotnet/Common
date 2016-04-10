namespace Framework.Common.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class AggregateRoot<TPrimaryKey> : Entity<TPrimaryKey>, IAggregateRoot<TPrimaryKey>
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        protected AggregateRoot() { }

        /// <summary>
        /// 构造方法。
        /// </summary>
        protected AggregateRoot(TPrimaryKey id) : base(id) { }
    }
}