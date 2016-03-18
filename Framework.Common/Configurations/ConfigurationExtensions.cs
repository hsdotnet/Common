using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Framework.Common.Serialization;
using Framework.Common.Ioc;
using Framework.Common.Logger;

namespace Framework.Common.Configurations
{
    public static class ConfigurationExtensions
    {
        public static Configuration UseAutofac(this Configuration configuration)
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer());
            return configuration;
        }

        public static Configuration UseJsonNet(this Configuration configuration)
        {
            configuration.SetDefault<IObjectSerializer, JsonObjectSerializer>(new JsonObjectSerializer());
            return configuration;
        }

        public static Configuration UseLog4Net(this Configuration configuration, string configFile = "log4net.config")
        {
            configuration.SetDefault<ILoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile));
            return configuration;
        }
    }
}