using System;
using Insane.Notifications.iOS;
using Insane.Notifications.iOS.Presenter;
using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.Plugins.Notifications.Sample.Portable;
using MvvmCross.Platform;
using Insane.Notifications.Presenter;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.CachedStorage;
using Insane.Notifications.PushSample.iOS.Services;

namespace Insane.Notifications.PushSample.iOS
{
    public class AppSetup : MvxIosSetup
    {
        public AppSetup(IMvxApplicationDelegate applicationDelegate, UIKit.UIWindow window) : base(applicationDelegate, window)
        {
        }

        public AppSetup(IMvxApplicationDelegate applicationDelegate, MvvmCross.iOS.Views.Presenters.IMvxIosViewPresenter presenter) : base(applicationDelegate, presenter)
        {
        }
		

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.RegisterSingleton<RemotePushNotificationService>(() => {
				var pushTagsProvider = Mvx.Resolve<IPushTagsProvider>();
				var remotePushRegistrationService = Mvx.Resolve<IRemotePushRegistrationService>();

                return new iOSRemotePushNotificationServiceIos(remotePushRegistrationService, pushTagsProvider);
            });

            Mvx.RegisterSingleton<INotificationsService>(() => Mvx.Resolve<RemotePushNotificationService>());

            Mvx.RegisterSingleton<IRemoteNotificationIdProvider>(() => new AppNotificationsIdProvider());

            Mvx.RegisterSingleton<IIOSRemoteNotificationsPresenter>(() => new iOSRemoteNotificationPresenter(
                Mvx.Resolve<IRemoteNotificationIdProvider>(),
                typeof(AppSetup).Assembly
            ));
                                                                    
			PushiOSNotificationsSetup.Initialize(Mvx.Resolve<IIOSRemoteNotificationsPresenter>());
		}

        protected override IMvxApplication CreateApp()
        {
            return new MvxApp();
        }
    }
}
