using System;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Copper.Core;
using Copper.Core.Data;
using Copper.Core.Infrastructure;
using Copper.Web.Framework.Validators;
using FluentValidation.Mvc;

namespace Copper.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            
            //初始化引擎（重新创建工厂）
            EngineContext.Initialize(false);

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //fluent validation
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new CopperValidatorFactory()));

            var databaseInstalled = DataSettingsHelper.DatabaseIsInstalled();

            //log application start
            if (databaseInstalled)
            {
                //LogHelper.Log4Net.Info("Application started");
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            //ignore static resources
            if (webHelper.IsStaticResource(this.Request))
                return;
            //ensure database is installed
            if (!DataSettingsHelper.DatabaseIsInstalled())
            {
                var installUrl = $"{webHelper.GetLocation()}install";
                if (!webHelper.GetThisPageUrl(false).StartsWith(installUrl, StringComparison.InvariantCultureIgnoreCase))
                {
                    this.Response.Redirect(installUrl);
                }
            }

        }
        protected void Application_Error(Object sender, EventArgs e)
        {
            var exception = Server.GetLastError();
            //LogHelper.Log4Net.Error(exception.Message);
        }
    }
}
