namespace Framework.Common.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class Command<TPrimaryKey> : ICommand<TPrimaryKey>
    {
        public TPrimaryKey Id { get; protected set; }

        public string AggregateRoot { get; private set; }

        public Command()
        {
        }

        public Command(string aggregateRoot)
        {
            this.AggregateRoot = aggregateRoot;
        }

        public Command(TPrimaryKey id, string aggregateRoot)
        {
            this.Id = id;
            this.AggregateRoot = aggregateRoot;
        }

        public virtual void SetCommandId(TPrimaryKey commandId)
        {
            this.Id = commandId;
        }
    }
}