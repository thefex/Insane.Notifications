using UserNotifications;

namespace Insane.Notifications.iOS.Presenter.Handlers.Badge
{
    public interface IIOSBadgeNotificationHandler
    {
        /// <summary>
        /// Handles the badge notification.
        /// </summary>
        /// <returns><c>true</c>, if badge notification was handled, <c>false</c> otherwise.</returns>
        /// <param name="badgeNumber">Badge number.</param>
        /// <param name="completeNotification">Complete notification.</param>
        bool HandleBadgeNotification(int badgeNumber, UNNotification completeNotification);
    }
}
