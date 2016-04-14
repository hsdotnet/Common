namespace Framework.Common.Commands
{
    public interface ICommandUnitOfWork
    {
        void Commit();

        void Rollback();
    }
}
