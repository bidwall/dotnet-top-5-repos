using System.Web.Mvc;
using HttpClientHelpers;
using Repositories;
using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;

namespace WebApp
{
    public static class IoCConfig
    {
        public static void RegisterDependencies()
        {
            var container = new Container();

            container.Register<IRepository, GitHubRepository>();
            container.Register<IHttpClientHelper, GitHubHttpClientHelper>();
            container.Register<IHttpResponseProvider, HttpResponseProvider>();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
}