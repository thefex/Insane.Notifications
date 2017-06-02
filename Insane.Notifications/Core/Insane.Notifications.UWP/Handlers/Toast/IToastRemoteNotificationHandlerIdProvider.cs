using Windows.UI.Notifications;

namespace InsaneNotifications.UWP.Handlers.Toast
{
    public interface IToastRemoteNotificationHandlerIdProvider
    {
        string GetToastNotificationHandlerId(ToastNotification toastNotification);
    }
}
