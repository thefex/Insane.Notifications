using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Sample.Portable.Services;

namespace MvvmCross.Plugins.Notifications.Sample.Portable
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
