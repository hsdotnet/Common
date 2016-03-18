using System.Collections.Generic;

namespace Framework.Common.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectContainer
    {
        private static IObjectContainer _container;

        public static void SetContainer(IObjectContainer container)
        {
            _container = container;
        }

        public static void InitializeFromConfigFile(string sectionName = null)
        {
            _container.InitializeFromConfigFile(sectionName);
        }

        public static void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.WeakReferenceRequest)
            where TService : class
            where TImplementer : class, TService
        {
            _container.Register<TService, TImplementer>(serviceName, life);
        }

        public static TService Resolve<TService>(string serviceName = null) where TService : class
        {
            return _container.Resolve<TService>(serviceName);
        }

        public static IEnumerable<TService> ResolveAll<TService>()
        {
            return _container.ResolveAll<TService>();
        }
    }
}