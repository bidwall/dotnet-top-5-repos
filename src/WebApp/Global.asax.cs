using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HttpClientHelpers;
using Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace WebApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SetupIoC();
        }

        private static void SetupIoC()
        {
            var container = new Container();

            container.Register<IRepository, GitHubRepository>();
            container.Register<IHttpClientHelper, GitHubHttpClientHelper>();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}
