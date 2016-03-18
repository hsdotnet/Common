using Quartz;

namespace Framework.Common.Scheduler
{
    public abstract class BaseJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            this.DoWork();
        }

        public abstract void DoWork();
    }
}