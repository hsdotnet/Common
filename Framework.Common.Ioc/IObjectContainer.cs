using System.Collections.Generic;

namespace Framework.Common.Ioc
{
    /// <summary>
    /// 
    /// </summary>
    public interface IObjectContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        void InitializeFromConfigFile(string sectionName = null);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.WeakReferenceRequest)
            where TService : class
            where TImplementer : class, TService;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="instance"></param>
        /// <param name="serviceName"></param>
        void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        TService Resolve<TService>(string serviceName = null) where TService : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        IEnumerable<TService> ResolveAll<TService>();
    }
}