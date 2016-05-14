using System;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using Copper.Core;
using Copper.Core.Caching;
using Copper.Core.Data;
using Copper.Core.Fakes;
using Copper.Core.Infrastructure;
using Copper.Core.Infrastructure.DependencyManagement;
using Copper.Core.Plugins;
using Copper.Data;
using Copper.Web.Framework.Mvc.Routes;

namespace Copper.Web.Framework
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //HTTP context and other related stuff
            builder.Register(c =>
              //register FakeHttpContext when HttpContext is not available
              HttpContext.Current != null ?
              (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
              (new FakeHttpContext("~/") as HttpContextBase))
              .As<HttpContextBase>()
              .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
               .As<HttpRequestBase>()
               .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();
            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>();

            //注册WebWorkContext
            builder.RegisterType<WebWorkContext>().As<IWorkContext>();

            //routes
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance();
            //controllers
            var assemblies = typeFinder.GetAssemblies().ToArray();
            builder.RegisterControllers(assemblies);

            //data layer
            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            builder.Register(x => new CopperDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();
            
            builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            if (dataProviderSettings != null && dataProviderSettings.IsValid())
            {
                builder.Register<ICopperDbConnection>(c => new CopperDbConnection(dataProviderSettings.DataProvider, dataProviderSettings.DataConnectionString)).InstancePerLifetimeScope();
            }
            else
            {
                builder.Register<ICopperDbConnection>(c => new CopperDbConnection(dataSettingsManager.LoadSettings().DataProvider, dataSettingsManager.LoadSettings().DataConnectionString)).InstancePerLifetimeScope();
                //builder.Register<ICopperDbConnection>(c => new CopperDbConnection(c.Resolve<DataSettings>().DataProvider, c.Resolve<DataSettings>().DataConnectionString)).InstancePerLifetimeScope();
            }

            //plugins
            builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerLifetimeScope();
            //注册缓存
            builder.RegisterType(typeof(MemoryCacheManager)).As(typeof(ICacheManager)).SingleInstance();
            //注册Repository
            var repositoryAssembly = Assembly.Load("Copper.Data");
            builder.RegisterAssemblyTypes(repositoryAssembly).Where(c => c.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();

            //注册Services
            var serviceAssembly = Assembly.Load("Copper.Services");
            builder.RegisterAssemblyTypes(serviceAssembly).Where(c => c.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
