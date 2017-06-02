using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers
{
    public interface IToastRemoteNotificationHandlerIdProvider
    {
        string GetToastNotificationHandlerId(ToastNotification toastNotification);
    }
}
