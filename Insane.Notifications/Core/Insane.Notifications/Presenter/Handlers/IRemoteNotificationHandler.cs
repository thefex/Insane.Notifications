namespace MvvmCross.Plugins.Notifications.Presenter.Handlers
{
    public interface IRemoteNotificationHandler
    {
        bool Handle(string notificationJson, string notificationId);
    }
}
