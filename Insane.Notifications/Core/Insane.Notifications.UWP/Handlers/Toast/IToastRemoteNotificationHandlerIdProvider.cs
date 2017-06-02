using Windows.UI.Notifications;

namespace Insane.Notifications.UWP.Handlers.Toast
{
    public interface IToastRemoteNotificationHandlerIdProvider
    {
        string GetToastNotificationHandlerId(ToastNotification toastNotification);
    }
}
