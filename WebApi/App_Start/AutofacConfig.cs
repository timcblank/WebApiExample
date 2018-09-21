using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Integration.WebApi;
using Services;
using System.Web;
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
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        public static IContainer RegisterServices(ContainerBuilder builder)
        {
            // this is a reflection service that will loop through asemblies in the application and register them with autofac
            var reflectionBuilder = new AutofacRegistrationStart();
            reflectionBuilder.Execute(builder);
            Container = builder.Build();
            return Container;
        }
    }

    /// <summary>
    /// AutofacRegistrationStart loads and loops through all the assemblies to put into the build container for autofac
    /// </summary>
    public class AutofacRegistrationStart : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .Where(t => t.Name.EndsWith("Controller"));
        }

        public void Execute(ContainerBuilder builder)
        {
            // create a singleton instance of the logger class
            builder.RegisterType<Logger>().As<ILogger>().SingleInstance();

            // call the loader to load up all assemblies for registration
            this.Load(builder);
        }
    }
}
