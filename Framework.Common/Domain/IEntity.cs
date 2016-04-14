namespace Framework.Common.Domain
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; }
    }

    /// <summary>
    /// 默认实体 类型 int
    /// </summary>
    public interface IEntity : IEntity<int>
    {
    }
}