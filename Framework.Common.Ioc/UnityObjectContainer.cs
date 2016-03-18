using System.Configuration;
using System.Collections.Generic;

using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Framework.Common.Ioc
{
    /// <summary>
    /// Unity 注入
    /// </summary>
    public class UnityObjectContainer : IObjectContainer
    {
        private readonly IUnityContainer _container;

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public UnityObjectContainer()
        {
            this._container = new UnityContainer();
        }
        #endregion

        #region IObjectContainer 成员
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName"></param>
        public void InitializeFromConfigFile(string sectionName = null)
        {
            if (string.IsNullOrWhiteSpace(sectionName)) { sectionName = UnityConfigurationSection.SectionName; }

            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection(sectionName);

            section.Configure(_container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.WeakReferenceRequest)
            where TService : class
            where TImplementer : class, TService
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                this._container.RegisterType<TService, TImplementer>(GetLifetimeManager(life));
            else
                this._container.RegisterType<TService, TImplementer>(serviceName, GetLifetimeManager(life));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public TService Resolve<TService>(string serviceName = null) where TService : class
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return this._container.Resolve<TService>();
            else
                return this._container.Resolve<TService>(serviceName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public IEnumerable<TService> ResolveAll<TService>()
        {
            return this._container.ResolveAll<TService>();
        }
        #endregion

        #region Private Method
        /// <summary>
        /// 
        /// </summary>
        /// <param name="life"></param>
        /// <returns></returns>
        private LifetimeManager GetLifetimeManager(LifeStyle life)
        {
            switch (life)
            {
                case LifeStyle.Transient:
                    return new TransientLifetimeManager();
                case LifeStyle.WeakReferenceRequest:
                    return new ExternallyControlledLifetimeManager();
                case LifeStyle.Singleton:
                    return new ContainerControlledLifetimeManager();
                case LifeStyle.InThread:
                    return new PerThreadLifetimeManager();
                default:
                    return new ExternallyControlledLifetimeManager();
            }
        }
        #endregion
    }
}