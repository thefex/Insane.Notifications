namespace MvvmCross.Plugins.Notifications.IOS.Presenter.Handlers
{
    public interface IRemoteNotificationHandler
    {
        bool Handle(string notificationJson, string notificationId);
    }
}