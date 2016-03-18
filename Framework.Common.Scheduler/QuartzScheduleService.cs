using System.Collections.Generic;

using Quartz;
using Quartz.Impl;

namespace Framework.Common.Scheduler
{
    /// <summary>
    /// 
    /// </summary>
    public class QuartzScheduleService : IScheduleService
    {
        private readonly Dictionary<string, IScheduler> _taskDict = new Dictionary<string, IScheduler>();

        public void StartTask<Job>(string name, int seconds) where Job : BaseJob
        {
            if (_taskDict.ContainsKey(name)) { return; }

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            IJobDetail job = JobBuilder.Create<Job>().Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(seconds).RepeatForever()).Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();

            _taskDict.Add(name, scheduler);
        }

        public void StopTask(string name)
        {
            IScheduler scheduler;

            if (_taskDict.TryGetValue(name, out scheduler))
            {
                scheduler.Shutdown(true);
            }
        }
    }
}