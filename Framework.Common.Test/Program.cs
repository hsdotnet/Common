using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Framework.Common.Serialization;
using Framework.Common.Configurations;
using Framework.Common.Logger;
using Framework.Common.Ioc;
using Framework.Common.Scheduler;
using Framework.Common.Helper;

namespace Framework.Common.Test
{

    class Program
    {
        private static ILogger _logger;

        private static IObjectSerializer _serializer;

        private static IScheduleService _schedule;



        static void Main(string[] args)
        {
            //string str = null;

            //EnsureHelper.NotNull(str, "用户名");

            Init();

            _schedule = ObjectContainer.Resolve<IScheduleService>();

            JobInfo jobInfo = new JobInfo()
            {
                JobId = 1,
                Description = "测试",
                JobSeconds = 5,
                IsValid = true,
                JobName = "Test",
                JobUrl = "http://localhost:13194/login/job",
                Status = JobStatus.Init
            };

            _schedule.StartTask<HttpJob>(jobInfo);
        }

        static void Method2()
        {
            using (StreamWriter sw = new StreamWriter(@"F:\Code\dotnet\Common\Framework.Common.Test\bin\Debug\logs\a.txt", true, Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        static void Method1()
        {
            _logger = ObjectContainer.Resolve<ILoggerFactory>().CreateLogger(typeof(Program).Name);

            _serializer = ObjectContainer.Resolve<IObjectSerializer>();

            Student student = new Student() { Id = 10, Name = "张三" };

            string json = _serializer.SerializeObject(student);

            Console.WriteLine(json);

            _logger.Info("测试", "哈哈");
        }

        static void Init()
        {
            Configuration.GetInstance()
               .UseAutofac()
               .UseJsonNet()
               .UseLog4Net()
               .RegisterCommonComponents()
               .SetDefault<IScheduleService, QuartzScheduleService>();
        }
    }

    public class OrderJob : BaseJob
    {
        public override void DoWork(JobInfo job)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            //ObjectContainer.Resolve<IScheduleService>().StopTask("测试");
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}