using System;

using Quartz;

namespace Framework.Common.Scheduler
{
    public abstract class BaseJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap data = context.JobDetail.JobDataMap;

            JobInfo jobInfo = data.Get("JobInfo") as JobInfo;

            if (jobInfo == null)
                throw new ArgumentNullException("Job未找到");

            if (!jobInfo.IsValid) { throw new Exception(string.Format("Job Id:【{0}】 Name:【{1}】已失效", jobInfo.JobId, jobInfo.JobName)); }

            this.DoWork(jobInfo);
        }

        public abstract void DoWork(JobInfo jobInfo);
    }
}