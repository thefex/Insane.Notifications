using System;

namespace MvvmCross.Plugins.Notifications.Presenter
{
    public interface IRemoteNotificationIdProvider
    {
        string GetNotificationId(string notificationJson);
    }
}
