using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Services;
using System.Web.Mvc;
using System.Web.Http;
using Controllers;
using Repositories;

namespace WebApi
{
    /// <summary>
    /// Autofac Config static class to register assemblies within the application with Autofac for dependency injection
    /// </summary>
    public static class AutofacConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Initialize()
        {
            var container = RegisterServices(new ContainerBuilder());
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            // this is a reflection service that will loop through asemblies in the application and register them with autofac
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Controller"));

            var container = builder.Build();
            return container;
        }
    }
}
