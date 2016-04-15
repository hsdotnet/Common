namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令服务接口
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <typeparam name="TCommand">命令类型</typeparam>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="command">命令对象</param>
        /// <returns>命令结果对象</returns>
        CommandResult<TPrimaryKey> Execute<TCommand, TPrimaryKey>(TCommand command) where TCommand : ICommand<TPrimaryKey>;
    }
}