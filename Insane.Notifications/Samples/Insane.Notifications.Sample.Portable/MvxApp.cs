using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Sample.Portable.Services;
using MvvmCross.Plugins.Notifications.Sample.Portable.ViewModels;

namespace MvvmCross.Plugins.Notifications.Sample.Portable
{
    public class MvxApp : MvxApplication
    {

        public override void Initialize()
        {
            base.Initialize();

			Mvx.RegisterType<IMvxAppStart, MvxAppStart<MainViewModel>>();
	        Mvx.RegisterSingleton(() => new LocalNotificationsProvider());
        }
    }
}
