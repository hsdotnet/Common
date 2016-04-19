namespace Framework.Common.Scheduler
{
    public interface IScheduleService
    {
        void StartTask<Job>(JobInfo jobInfo) where Job : BaseJob;

        void StopTask(JobInfo jobInfo);
    }
}