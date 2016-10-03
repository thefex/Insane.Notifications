using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications;
using NotificationsSample.Portable.Services;

namespace NotificationsSample.Portable
{
    public class MvxApp : MvxApplication
    {

        public override void Initialize()
        {
            base.Initialize();

	        Mvx.RegisterSingleton(() => new LocalNotificationsProvider());
        }
    }
}
