using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using log4net.Appender;
using log4net.Layout;
using log4net.Config;
using log4net;

namespace Framework.Common.Logger
{
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        public Log4NetLoggerFactory(string configFile = "log4net.config")
        {
            FileInfo file = new FileInfo(configFile);
            if (!file.Exists)
                file = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile));

            if (file.Exists)
                XmlConfigurator.ConfigureAndWatch(file);
            else
                BasicConfigurator.Configure(new ConsoleAppender { Layout = new PatternLayout() });
        }

        public ILogger CreateLogger(string name)
        {
            return new Log4NetLogger(LogManager.GetLogger(name));
        }

        public ILogger CreateLogger(Type type)
        {
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}
