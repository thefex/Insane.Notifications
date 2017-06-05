using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Insane.PushSample.Backend.Services;
using Insane.PushSample.Backend.Services.Concrete;

namespace Insane.PushSample.Backend
{
    public class Bootstrapper
    {
        public static void Run()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DefaultTagSubscriptionService>().InstancePerDependency();
            //builder.RegisterType<NotificationHubClient>().InstancePerDependency();
            builder.RegisterType<NotificationHubRegistrationServices>().InstancePerDependency();
            //builder.RegisterType<TagSubscriptionService>().InstancePerDependency();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var resolver = new AutofacWebApiDependencyResolver(builder.Build());
            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }
    }
}