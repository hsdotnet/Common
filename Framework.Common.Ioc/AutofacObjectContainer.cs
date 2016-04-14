using System;
using System.Collections.Generic;

using Autofac;

namespace Framework.Common.Ioc
{
    /// <summary>
    /// Autofac 注入
    /// </summary>
    public class AutofacObjectContainer : IObjectContainer
    {
        private readonly IContainer _container;

        #region Ctor
        /// <summary>
        /// 
        /// </summary>
        public AutofacObjectContainer()
        {
            this._container = new ContainerBuilder().Build();
        }
        #endregion

        #region IObjectContainer 成员

        public void InitializeFromConfigFile(string sectionName = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ContainerBuilder _builder = new ContainerBuilder();

            var registrationBuilder = _builder.RegisterType<TImplementer>().As<TService>();

            if (serviceName != null) { registrationBuilder.Named<TService>(serviceName); }

            switch (life)
            {
                case LifeStyle.Transient:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeStyle.WeakReferenceRequest:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
                case LifeStyle.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
                case LifeStyle.InThread:
                    registrationBuilder.InstancePerRequest();
                    break;
                default:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
            }

            _builder.Update(_container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementer"></typeparam>
        /// <param name="instance"></param>
        /// <param name="serviceName"></param>
        /// <param name="life"></param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            ContainerBuilder builder = new ContainerBuilder();

            var registrationBuilder = builder.RegisterInstance(instance).As<TService>();

            if (serviceName != null) { registrationBuilder.Named<TService>(serviceName); }

            switch (life)
            {
                case LifeStyle.Transient:
                    registrationBuilder.InstancePerDependency();
                    break;
                case LifeStyle.WeakReferenceRequest:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
                case LifeStyle.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
                case LifeStyle.InThread:
                    registrationBuilder.InstancePerRequest();
                    break;
                default:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
            }

            builder.Update(_container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public TService Resolve<TService>(string serviceName = null) where TService : class
        {
            if (serviceName == null)
                return this._container.Resolve<TService>();
            else
                return this._container.ResolveNamed<TService>(serviceName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public IEnumerable<TService> ResolveAll<TService>()
        {
            return this._container.Resolve<IEnumerable<TService>>();
        }
        #endregion
    }
}