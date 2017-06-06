using MvvmCross.Droid.Platform;
using Android.Content;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.Notifications.Sample.Portable;
using MvvmCross.Platform;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.Droid.GCM;
using Insane.Notifications.Presenter;
using Insane.Notifications.PushSample.Droid.Services;
using Acr.UserDialogs;
using MvvmCross.Platform.Droid.Platform;
using Insane.Notifications.CachedStorage;

namespace Insane.Notifications.PushSample.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public Setup(Context applicationContext) : base(applicationContext)
        {
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.RegisterType<Context>(() => this.ApplicationContext);
            Mvx.RegisterSingleton<RemotePushNotificationService>(() => {

            return new GcmRemotePushNotificationService(
                    Mvx.Resolve<IRemotePushRegistrationService>(),
                    Mvx.Resolve<IPushTagsProvider>(),
                    Mvx.Resolve<Context>(),
                    Constants.PushSenderId
                );
            });
            Mvx.RegisterSingleton<IRemoteNotificationIdProvider>(() => new GcmPushNotificationIdProvider());

            Mvx.RegisterSingleton<INotificationsService>(() => Mvx.Resolve<RemotePushNotificationService>());

            Mvx.RegisterSingleton<IRemoteNotificationsPresenter>(() =>
            {
                return new RemoteNotificationsPresenter(
                    Mvx.Resolve<IRemoteNotificationIdProvider>(),
                    typeof(Setup).Assembly
                );
            });

            UserDialogs.Init(() => Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity);
		}

        protected override IMvxApplication CreateApp()
        {
            return new MvxApp();
        }
    }
}

