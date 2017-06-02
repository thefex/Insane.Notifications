using System;

namespace InsaneNotifications.UWP.Handlers
{
    public interface IRemoteNotificationTapAction
    {
        /// <summary>
        /// Invoked when the notification is tapped.
        /// </summary>
        /// <param name="notificationDataJson">Notification data json.</param>
        void OnNotificationTapped(string notificationDataJson);
    }
}
