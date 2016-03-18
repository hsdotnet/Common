namespace Framework.Common.Scheduler
{
    public interface IScheduleService
    {
        void StartTask<Job>(string name, int seconds) where Job : BaseJob;

        void StopTask(string name);
    }
}