using System;
using MvvmCross.Plugins.Notifications.Data;

namespace MvvmCross.Plugins.Notifications.Presenter
{
    public interface IRemoteNotificationsPresenter
    {
        void HandleNotification(string notificationJson);
        event Action<RemoteNotificationData> NotificationPublished;
    }
}