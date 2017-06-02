using System;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers
{
    public interface IIOSSoundNotificationHandler
    {
        /// <summary>
        /// Handles the sound notification.
        /// </summary>
        /// <returns><c>true</c>, if sound notification was handled, <c>false</c> otherwise.</returns>
        /// <param name="soundNotificationData">Sound notification data.</param>
        /// <param name="completeNotification">Complete notification.</param>
        bool HandleSoundNotification(UNNotificationSound soundNotificationData, UNNotification completeNotification);
    }
}
