using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Copper.Core.Infrastructure.DependencyManagement;

namespace Copper.Core.Infrastructure
{
    public class CopperEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;


        #endregion

        #region Utilities
        /// <summary>
        /// Run startup tasks
        /// </summary>
        protected virtual void RunStartupTasks()
        {
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            var startUpTasks = new List<IStartupTask>();
            foreach (var startUpTaskType in startUpTaskTypes)
                startUpTasks.Add((IStartupTask)Activator.CreateInstance(startUpTaskType));
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            foreach (var startUpTask in startUpTasks)
                startUpTask.Execute();
        }

        /// <summary>
        /// Register dependencies
        /// </summary>
        protected void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<AppDomainTypeFinder>().As<ITypeFinder>().SingleInstance();
            var container = builder.Build();

            builder.RegisterInstance(this).As<IEngine>().SingleInstance();

            var typeFinder = new AppDomainTypeFinder();
            builder = new ContainerBuilder();
            var drTypes = typeFinder.FindClassesOfType<IDependencyRegistrar>();
            var drInstances = new List<IDependencyRegistrar>();
            foreach (var drType in drTypes)
                drInstances.Add((IDependencyRegistrar)Activator.CreateInstance(drType));
            //sort
            drInstances = drInstances.AsQueryable().OrderBy(t => t.Order).ToList();
            foreach (var dependencyRegistrar in drInstances)
                dependencyRegistrar.Register(builder, typeFinder);
            builder.Update(container);
            //var container = builder.Build();
            this._containerManager = new ContainerManager(container);
            //设置依赖注入解析器
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        #endregion

        #region Methods

        /// <summary>
        /// Initialize components and plugins in the nop environment.
        /// </summary>
        public void Initialize()
        {
            //register dependencies
            RegisterDependencies();

            //startup tasks
            RunStartupTasks();
        }

        /// <summary>
        /// Resolve dependency
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        /// <summary>
        ///  Resolve dependency
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        /// <summary>
        /// Resolve dependencies
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns></returns>
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        #endregion

        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }
    }
}
