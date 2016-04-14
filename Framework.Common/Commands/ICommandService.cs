namespace Framework.Common.Commands
{
    public interface ICommandService
    {
        CommandResult<TPrimaryKey> Execute<TCommand, TPrimaryKey>(TCommand command) where TCommand : ICommand<TPrimaryKey>;
    }
}