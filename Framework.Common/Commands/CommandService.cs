using System;

using Framework.Common.Helper;
using Framework.Common.Ioc;
using Framework.Common.Logger;

namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令服务
    /// </summary>
    public class CommandService : ICommandService
    {
        /// <summary>
        /// 执行命令
        /// </summary>
        /// <typeparam name="TCommand">命令类型</typeparam>
        /// <typeparam name="TPrimaryKey">主键类型</typeparam>
        /// <param name="command">命令对象</param>
        /// <returns>命令结果对象</returns>
        public CommandResult Execute<TCommand, TPrimaryKey>(TCommand command) where TCommand : ICommand<TPrimaryKey>
        {
            ICommandContext context = ObjectContainer.Resolve<ICommandContext>();

            CommandResult commandResult;

            try
            {
                ICommandHandler<TCommand, TPrimaryKey> commandHandler = ObjectContainer.Resolve<ICommandHandler<TCommand, TPrimaryKey>>();

                TimeSpan ts = ActionHelper.StopwatchAction(() =>
                {
                    commandHandler.Handler(context, command);

                    context.Commit();
                });

                commandResult = new CommandResult(command.AggregateRoot, command.Id.ToString(), CommandStatus.Success, ts.Milliseconds);
            }
            catch (Exception ex)
            {
                ObjectContainer.Resolve<ILoggerFactory>().CreateLogger(typeof(CommandService)).Error("CommandService执行出错", ex);

                context.Rollback();

                commandResult = new CommandResult(command.AggregateRoot, CommandStatus.Failed, result: ex.Message);
            }
            finally
            {
                context.Dispose();
            }

            this.SaveCommand(commandResult);

            return commandResult;
        }

        /// <summary>
        /// 保存命令
        /// </summary>
        /// <typeparam name="TPrimaryKey">命令主键类型</typeparam>
        /// <param name="command">命令对象</param>
        private void SaveCommand(CommandResult command)
        {
            new CommandResultContext().AddCommand(command);
        }
    }
}