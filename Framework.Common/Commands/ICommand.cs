namespace Framework.Common.Commands
{
    /// <summary>
    /// Command 接口
    /// </summary>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface ICommand<TPrimaryKey>
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        TPrimaryKey Id { get; }

        /// <summary>
        /// 聚合跟对象
        /// </summary>
        string AggregateRoot { get; }

        /// <summary>
        /// 设置命令主键Id
        /// </summary>
        /// <param name="commandId">命令主键Id</param>
        void SetCommandId(TPrimaryKey commandId);
    }
}