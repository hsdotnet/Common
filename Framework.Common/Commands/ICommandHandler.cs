namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令处理对象
    /// </summary>
    /// <typeparam name="TCommand">命令类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface ICommandHandler<TCommand, TPrimaryKey> where TCommand : ICommand<TPrimaryKey>
    {
        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="context">命令上下文对象</param>
        /// <param name="command">命令对象</param>
        void Handler(ICommandContext context, TCommand command);
    }
}