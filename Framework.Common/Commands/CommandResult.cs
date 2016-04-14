namespace Framework.Common.Commands
{
    public class CommandResult<TPrimaryKey>
    {
        public CommandStatus Status { get; private set; }

        public string Result { get; private set; }

        public string AggregateRoot { get; set; }

        public TPrimaryKey AggregateRootId { get; private set; }

        public int CommandTime { get; set; }

        public CommandResult()
        {
        }

        public CommandResult(string aggregateRoot, TPrimaryKey aggregateRootId, CommandStatus status, int commandTime = 0, string result = null)
        {
            this.AggregateRoot = aggregateRoot;
            this.AggregateRootId = aggregateRootId;
            this.Status = status;
            this.CommandTime = commandTime;
            this.Result = result;
        }

        public CommandResult(string aggregateRoot, CommandStatus status, int commandTime = 0, string result = null)
        {
            this.AggregateRoot = aggregateRoot;
            this.Status = status;
            this.CommandTime = commandTime;
            this.Result = result;
        }
    }
}