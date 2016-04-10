namespace Framework.Common.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IAggregateRoot<TPrimaryKey> : IEntity<TPrimaryKey>
    {
    }

    /// <summary>
    /// 默认聚合根 类型 int
    /// </summary>
    public interface IAggregateRoot : IAggregateRoot<int>, IEntity<int>, IEntity
    {
    }
}
