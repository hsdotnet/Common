using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Scheduler
{
    public class JobInfo
    {
        public int JobId { get; set; }

        public string JobName { get; set; }

        public int JobSeconds { get; set; }

        public string Description { get; set; }

        public JobStatus Status { get; set; }

        public string JobUrl { get; set; }

        public bool IsValid { get; set; }

        public void Run()
        {
            this.Status = JobStatus.Run;

            this.UpdateStatus();
        }

        public void Stop()
        {
            this.Status = JobStatus.Stop;

            this.UpdateStatus();
        }

        public void UpdateStatus()
        {
            new JobContext().UpdateStatus(this);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum JobStatus
    {
        /// <summary>
        /// 
        /// </summary>
        Init,
        /// <summary>
        /// 
        /// </summary>
        Run,
        /// <summary>
        /// 
        /// </summary>
        Stop
    }
}