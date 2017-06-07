using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Insane.Notifications.Presenter;
using Insane.Notifications.PushNotifications;
using Insane.Notifications.PushSample.UWP.Services.Handlers;
using Insane.Notifications.UWP;
using Insane.Notifications.UWP.Handlers.Badge;
using Insane.Notifications.UWP.Handlers.Tile;
using Insane.Notifications.UWP.Handlers.Toast;
using Insane.Notifications.UWP.Presenter;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Notifications.Sample.Portable;
using MvvmCross.Uwp.Platform;
using MvvmCross.Uwp.Views;

namespace Insane.Notifications.PushSample.UWP
{
    public class WindowsApplicationSetup : MvxWindowsSetup
    {
        public WindowsApplicationSetup(Frame rootFrame, string suspensionManagerSessionStateKey = null) : base(rootFrame, suspensionManagerSessionStateKey)
        {
        }

        public WindowsApplicationSetup(IMvxWindowsFrame rootFrame) : base(rootFrame)
        {
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();

            Mvx.RegisterSingleton<INotificationsService>(() =>
            {
                return new UWPRemotePushNotificationsService(
                        Mvx.Resolve<IRemotePushRegistrationService>(),
                        Mvx.Resolve<IPushTagsProvider>(),
                        Mvx.Resolve<IUniversalWindowsRemoteNotificationsPresenter>()
                    );
            });

            Mvx.RegisterSingleton<IUniversalWindowsRemoteNotificationsPresenter>(() =>
            {
                return new UniversalWindowsRemoteNotificationsPresenter(
                    Mvx.Resolve<IRemoteNotificationIdProvider>(),
                    Mvx.Resolve<UniversalWindowsPresenterConfiguration>(),
                    typeof(WindowsApplicationSetup).GetTypeInfo().Assembly);
            });

            Mvx.RegisterSingleton<UniversalWindowsPresenterConfiguration>(() =>
                {
                    return new UniversalWindowsPresenterConfiguration(
                        Mvx.Resolve<IBadgeRemoteNotificationHandlerIdProvider>(),
                        Mvx.Resolve<IToastRemoteNotificationHandlerIdProvider>(),
                        Mvx.Resolve<ITileRemoteNotificationHandlerIdProvider>());
                });

            Mvx.RegisterType<IRemoteNotificationIdProvider, UWPRemoteNotificationIdProvider>();
            Mvx.RegisterType<IBadgeRemoteNotificationHandlerIdProvider, BadgeNotificationsIdProvider>();
            Mvx.RegisterType<ITileRemoteNotificationHandlerIdProvider, TileNotificationsIdProvider>();
            Mvx.RegisterType<IToastRemoteNotificationHandlerIdProvider, ToastNotificationsIdProvider>();
        }

        protected override IMvxApplication CreateApp()
        {
            return new MvxApp();
        }
    }
}
