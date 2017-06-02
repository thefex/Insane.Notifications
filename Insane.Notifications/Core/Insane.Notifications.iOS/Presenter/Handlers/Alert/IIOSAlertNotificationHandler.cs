using UserNotifications;

namespace Insane.Notifications.iOS.Presenter.Handlers.Alert
{
    public interface IIOSAlertNotificationHandler
    {
        /// <summary>
        /// Handles the alert notification.
        /// </summary>
        /// <returns><c>true</c>, if alert notification was handled, <c>false</c> otherwise.</returns>
        /// <param name="notificationContent">Notification content.</param>
        /// <param name="completeNotificiation">Complete notificiation.</param>
        bool HandleAlertNotification(UNNotificationContent notificationContent, UNNotification completeNotificiation);
    }
}
