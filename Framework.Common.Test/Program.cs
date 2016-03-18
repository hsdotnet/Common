using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Framework.Common.Serialization;
using Framework.Common.Configurations;
using Framework.Common.Logger;
using Framework.Common.Ioc;

namespace Framework.Common.Test
{
    class Program
    {
        private static ILogger _logger;

        private static IObjectSerializer _serializer;

        static void Main(string[] args)
        {
            Init();

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
                .UseLog4Net();
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}