namespace Insane.Notifications.Presenter
{
    public interface IRemoteNotificationIdProvider
    {
        string GetNotificationId(string notificationJson);
    }
}
