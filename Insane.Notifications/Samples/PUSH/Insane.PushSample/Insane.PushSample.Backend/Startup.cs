using Insane.PushSample.Backend;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace Insane.PushSample.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //app.UseAutofacMiddleware(Bootstrapper.Build());
            //app.UseAutofacWebApi(GlobalConfiguration.Configuration);
        }
    }
}