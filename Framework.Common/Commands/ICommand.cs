namespace Framework.Common.Commands
{
    public interface ICommand<TPrimaryKey>
    {
        TPrimaryKey Id { get; }

        string AggregateRoot { get; }

        void SetCommandId(TPrimaryKey commandId);
    }
}