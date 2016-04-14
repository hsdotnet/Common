namespace Framework.Common.Commands
{
    public interface ICommandHandler<TCommand, TPrimaryKey> where TCommand : ICommand<TPrimaryKey>
    {
        void Handler(ICommandContext context, TCommand command);
    }
}