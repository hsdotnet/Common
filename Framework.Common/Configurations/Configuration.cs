using Framework.Common.Ioc;

namespace Framework.Common.Configurations
{
    public class Configuration
    {
        private static Configuration instance = new Configuration();

        private Configuration()
        {
        }

        public static Configuration GetInstance()
        {
            return instance;
        }

        public Configuration SetDefault<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.Register<TService, TImplementer>(serviceName, life);
            return this;
        }

        public Configuration SetDefault<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            ObjectContainer.RegisterInstance<TService, TImplementer>(instance, serviceName);
            return this;
        }

        public Configuration RegisterCommonComponents()
        {
            //SetDefault<IScheduleService, QuartzScheduleService>(null, LifeStyle.Transient);
            return this;
        }
    }
}