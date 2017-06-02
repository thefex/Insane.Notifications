using MvvmCross.Plugins.Notifications.Presenter;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS.Presenter
{
    public interface IIOSRemoteNotificationsPresenter : IRemoteNotificationsPresenter
    {
        /// <summary>
        /// Handles the notification.
        /// </summary>
        /// <returns>The notification.</returns>
        /// <param name="center">Center.</param>
        /// <param name="notification">Notification.</param>
        UNNotificationPresentationOptions HandleNotification(UNUserNotificationCenter center, UNNotification notification);

        /// <summary>
        /// Handles the notification tapped.
        /// </summary>
        /// <param name="notificationResponse">Notification response.</param>
        void HandleNotificationTapped(UNNotificationResponse notificationResponse);
    }
}
