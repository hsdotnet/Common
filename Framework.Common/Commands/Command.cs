namespace Framework.Common.Commands
{
    /// <summary>
    /// Command Abstract 基类
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public abstract class Command<TPrimaryKey> : ICommand<TPrimaryKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public TPrimaryKey Id { get; protected set; }

        /// <summary>
        /// 聚合跟对象
        /// </summary>
        public string AggregateRoot { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public Command()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aggregateRoot">聚合跟对象</param>
        public Command(string aggregateRoot)
        {
            this.AggregateRoot = aggregateRoot;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="aggregateRoot">聚合跟对象</param>
        public Command(TPrimaryKey id, string aggregateRoot)
            : this(aggregateRoot)
        {
            this.Id = id;
        }

        /// <summary>
        /// 设置命令主键Id
        /// </summary>
        /// <param name="commandId">命令主键Id</param>
        public virtual void SetCommandId(TPrimaryKey commandId)
        {
            this.Id = commandId;
        }
    }
}