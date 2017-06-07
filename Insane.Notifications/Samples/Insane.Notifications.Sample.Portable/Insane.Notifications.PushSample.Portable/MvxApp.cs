using System.Net.Http;
using Acr.UserDialogs;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.PushSample.Portable.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using Insane.Notifications.PushSample.Portable.Services;

namespace MvvmCross.Plugins.Notifications.Sample.Portable
{
    public class MvxApp : MvxApplication
    {

        public override void Initialize()
        {
            base.Initialize();

            Mvx.RegisterType<HttpClient>(() => new HttpClient(new HttpClientHandler()));
            Mvx.RegisterType<MainViewModel, MainViewModel>();
			Mvx.RegisterType<IMvxAppStart, MvxAppStart<MainViewModel>>();

            Mvx.RegisterType<IPushTagsProvider>(() => new PushTagsProviderService());

            Mvx.RegisterType<IRemotePushRegistrationService>(() => {
                return new BackendBasedRemotePushRegistrationService(new AppRestService());  
            });

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
		}
    }
}
