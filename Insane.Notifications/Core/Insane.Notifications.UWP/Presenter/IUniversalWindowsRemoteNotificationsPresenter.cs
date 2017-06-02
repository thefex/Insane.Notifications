using System;
using Windows.Networking.PushNotifications;
using Windows.UI.Notifications;
using Insane.Notifications.Presenter;

namespace Insane.Notifications.UWP.Presenter
{
    public interface IUniversalWindowsRemoteNotificationsPresenter : IRemoteNotificationsPresenter
    {
        void HandlePlatformNotification(PushNotificationReceivedEventArgs notificationArgs);
        void HandleRemoteNotificationActivation(string launchArgument);

        event Action<ToastNotification> ToastNotificationPublished;
        event Action<BadgeNotification> BadgeNotificationPublished;
        event Action<TileNotification> TileNotificationPublished;
    }
}