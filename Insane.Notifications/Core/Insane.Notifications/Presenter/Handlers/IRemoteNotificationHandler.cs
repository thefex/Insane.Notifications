namespace Insane.Notifications.Presenter.Handlers
{
    public interface IRemoteNotificationHandler
    {
        bool Handle(string notificationJson, string notificationId);
    }
}
