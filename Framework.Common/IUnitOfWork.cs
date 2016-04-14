namespace Framework.Common
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 是否在当前的工作单元模式中使用分布式事物 Transaction Coordinator (MS-DTC).
        /// </summary>
        bool DistributedTransactionSupported { get; }

        /// <summary>
        /// 当前的Unit Of Work事务是否已被提交。
        /// </summary>
        bool Committed { get; }

        /// <summary>
        /// 提交当前的Unit Of Work事务。
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚当前的Unit Of Work事务。
        /// </summary>
        void Rollback();
    }
}