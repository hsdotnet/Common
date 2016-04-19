using System;
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

        public void StartTask<Job>(JobInfo jobInfo) where Job : BaseJob
        {
            if (!jobInfo.IsValid) { throw new Exception(string.Format("Job Id:【{0}】 Name:【{1}】已失效", jobInfo.JobId, jobInfo.JobName)); }

            if (jobInfo.Status == JobStatus.Run) { return; }

            if (_taskDict.ContainsKey(jobInfo.JobName)) { return; }

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            IJobDetail job = JobBuilder.Create<Job>().Build();

            ITrigger trigger = TriggerBuilder.Create().StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(jobInfo.JobSeconds).RepeatForever()).Build();

            job.JobDataMap.Put("JobInfo", jobInfo);

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();

            jobInfo.Run();

            _taskDict.Add(jobInfo.JobName, scheduler);
        }

        public void StopTask(JobInfo jobInfo)
        {
            if (jobInfo.Status == JobStatus.Run)
            {
                IScheduler scheduler;

                if (_taskDict.TryGetValue(jobInfo.JobName, out scheduler))
                {
                    scheduler.Shutdown(true);

                    jobInfo.Stop();
                }
            }
        }
    }
}