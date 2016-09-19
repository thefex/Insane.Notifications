using System;
using Insane.Notifications.Data;

namespace Insane.Notifications.Presenter
{
    public interface IRemoteNotificationsPresenter
    {
        void HandleNotification(string notificationJson);
        event Action<RemoteNotificationData> NotificationPublished;
    }
}