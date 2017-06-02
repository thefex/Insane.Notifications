using System;
using MvvmCross.Plugins.Notifications.IOS.Internal;
using MvvmCross.Plugins.Notifications.Presenter.Handlers;
using UserNotifications;

namespace MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers
{
    public abstract class iOSRemoteNotificationTapAction : IRemoteNotificationTapAction
    {
        /// <summary>
        /// On the notification tapped with DEFAULT action.
        /// </summary>
        /// <param name="notificationDataJson">Notification data json.</param>
        public abstract void OnNotificationTapped(string notificationDataJson);

        public void OnNotificationTapped(string actionId, string notificationDataJson, UNNotificationResponse notificationResponse)
        {
            if (actionId == NotificationTappedHandler.DefaultActionIdentifier)
            {
                OnNotificationTapped(notificationDataJson);
                return;
            }

            if (actionId == NotificationTappedHandler.DismissActionIdentifier)
            {
                OnNotificationDismissed(notificationDataJson, notificationResponse);
                return;
            }

            OnNotificationTappedWithAction(actionId, notificationDataJson, notificationResponse);
        }

        /// <summary>
        /// On the notification dismissed.
        /// </summary>
        /// <param name="notificationDataJson">Notification data json.</param>
        /// <param name="notificationResponse">Notification response.</param>
        public virtual void OnNotificationDismissed(string notificationDataJson, UNNotificationResponse notificationResponse)
        {

        }

        /// <summary>
        /// On the notification tapped with action.
        /// </summary>
        /// <param name="actionId">Action identifier.</param>
        /// <param name="notificationDataJson">Notification data json.</param>
        /// <param name="notificationResponse">Notification response.</param>
        public virtual void OnNotificationTappedWithAction(string actionId, string notificationDataJson, UNNotificationResponse notificationResponse)
        {

        }
    }
}
