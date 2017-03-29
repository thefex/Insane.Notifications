namespace MvvmCross.Plugins.Notifications.IOS.NotificationsPresenter
{
    public interface IRemoteNotificationIdProvider
    {
        string GetNotificationId(string notificationJson);
    }
}