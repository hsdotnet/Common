namespace Framework.Common.Commands
{
    /// <summary>
    /// 命令状态
    /// </summary>
    public enum CommandStatus
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 未做改变
        /// </summary>
        NothingChanged = 2,
        /// <summary>
        /// 失败
        /// </summary>
        Failed = 3
    }
}