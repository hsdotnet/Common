using System;

using Framework.Common.Helper;
using Framework.Common.Ioc;
using Framework.Common.Logger;

namespace Framework.Common.Commands
{
    public class CommandService : ICommandService
    {
        public CommandResult<TPrimaryKey> Execute<TCommand, TPrimaryKey>(TCommand command) where TCommand : ICommand<TPrimaryKey>
        {
            ICommandContext context = ObjectContainer.Resolve<ICommandContext>();

            CommandResult<TPrimaryKey> commandResult;

            try
            {
                ICommandHandler<TCommand, TPrimaryKey> commandHandler = ObjectContainer.Resolve<ICommandHandler<TCommand, TPrimaryKey>>();

                TimeSpan ts = ActionHelper.StopwatchAction(() =>
                {
                    commandHandler.Handler(context, command);

                    context.Commit();
                });

                commandResult = new CommandResult<TPrimaryKey>(command.AggregateRoot, command.Id, CommandStatus.Success, ts.Milliseconds);
            }
            catch (Exception ex)
            {
                ObjectContainer.Resolve<ILoggerFactory>().CreateLogger(typeof(CommandService)).Error("CommandService执行出错", ex);

                context.Rollback();

                commandResult = new CommandResult<TPrimaryKey>(command.AggregateRoot, CommandStatus.Failed, result: ex.Message);
            }
            finally
            {
                context.Dispose();
            }

            this.SaveCommand(commandResult);

            return commandResult;
        }

        public void SaveCommand<TPrimaryKey>(CommandResult<TPrimaryKey> command)
        {
            new CommandResultContext().AddCommand(command);
        }
    }
}