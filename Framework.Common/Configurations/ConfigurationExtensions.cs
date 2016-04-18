using Framework.Common.Serialization;
using Framework.Common.Ioc;
using Framework.Common.Logger;
using Framework.Common.Cache;

namespace Framework.Common.Configurations
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseAutofac(this Configuration configuration)
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer());
            return configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseJsonNet(this Configuration configuration)
        {
            configuration.SetDefault<IObjectSerializer, JsonObjectSerializer>(new JsonObjectSerializer());
            return configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="configFile"></param>
        /// <returns></returns>
        public static Configuration UseLog4Net(this Configuration configuration, string configFile = "log4net.config")
        {
            configuration.SetDefault<ILoggerFactory, Log4NetLoggerFactory>(new Log4NetLoggerFactory(configFile));
            return configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static Configuration UseDefaultCache(this Configuration configuration)
        {
            configuration.SetDefault<ICache, DefaultCache>(new DefaultCache());
            return configuration;
        }
    }
}