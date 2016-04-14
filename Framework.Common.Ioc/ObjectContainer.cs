using System.Collections.Generic;

namespace Framework.Common.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public class ObjectContainer
    {
        public static IObjectContainer _container { get; private set; }

        public static void SetContainer(IObjectContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        public static void InitializeFromConfigFile(string sectionName = null)
        {
            _container.InitializeFromConfigFile(sectionName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public static void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.WeakReferenceRequest)
            where TService : class
            where TImplementer : class, TService
        {
            _container.Register<TService, TImplementer>(serviceName, life);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="instance"></param>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public static void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            _container.RegisterInstance<TService, TImplementer>(instance, serviceName, life);
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