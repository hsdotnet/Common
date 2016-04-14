namespace Framework.Common.Commands
{
    /// <summary>
    /// Command 工作单元
    /// </summary>
    public interface ICommandUnitOfWork
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}
