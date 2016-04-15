using System;

namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令结果对象
    /// </summary>
    public class CommandResult
    {
        /// <summary>
        /// 命令状态
        /// </summary>
        public CommandStatus Status { get; private set; }

        /// <summary>
        /// 执行结果
        /// </summary>
        public string Result { get; private set; }

        /// <summary>
        /// 聚合根对象
        /// </summary>
        public string AggregateRoot { get; set; }

        /// <summary>
        /// 聚合根主键
        /// </summary>
        public string AggregateRootId { get; private set; }

        /// <summary>
        /// 命令执行时间
        /// </summary>
        public int CommandTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommandResult()
        {
            this.Status = CommandStatus.None;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aggregateRoot">聚合根对象</param>
        /// <param name="status">命令状态</param>
        /// <param name="commandTime">命令执行时间</param>
        /// <param name="result">执行结果</param>
        public CommandResult(string aggregateRoot, CommandStatus status, int commandTime = 0, string result = null)
            : this()
        {
            this.AggregateRoot = aggregateRoot;
            this.Status = status;
            this.CommandTime = commandTime;
            this.Result = result;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="aggregateRoot">聚合根对象</param>
        /// <param name="aggregateRootId">聚合根主键</param>
        /// <param name="status">命令状态</param>
        /// <param name="commandTime">命令执行时间</param>
        /// <param name="result">执行结果</param>
        public CommandResult(string aggregateRoot, string aggregateRootId, CommandStatus status, int commandTime = 0, string result = null)
            : this(aggregateRoot, status, commandTime, result)
        {
            this.AggregateRootId = aggregateRootId;
        }
    }
}