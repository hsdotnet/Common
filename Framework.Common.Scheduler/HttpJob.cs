using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Scheduler
{
    public class HttpJob : BaseJob
    {
        public override void DoWork(JobInfo job)
        {
            Console.WriteLine(job.JobUrl);
        }
    }
}